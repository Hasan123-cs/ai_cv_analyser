using Ai_Cv_Analyser.Model;
using Ai_Cv_Analyser.Model.DTO;
using Ai_Cv_Analyser.Services.Interfaces;
using AICVAnalyzer.Data;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text.Json;
namespace Ai_Cv_Analyser.Services
{
    public class AiService : IAiService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _db;
        public AiService(HttpClient httpClient,IConfiguration configuration,ApplicationDbContext d)
        {
            _db = d;
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task<AnalysisResultDto> AnalyzeCVAsync(string cvText, CV cv)
        {
            Console.WriteLine($"CV Length: {cvText.Length}");
            // get the key 
            var apiKey = Environment.GetEnvironmentVariable("OPENROUTER_API_KEY");
            Console.WriteLine(apiKey?.Substring(0, 15));
            // prepare the prompt 
            var prompt = $@"
                Analyze this CV.
                
                Return ONLY valid JSON.
                
                {{
                    ""Score"":number,
                    ""Summary"":"""",
                    ""Strengths"":[],
                    ""Weaknesses"":[],
                    ""SuggestedProjects"":[],
                    ""SuggestedSkills"":[],
                    ""Recommendation"":""""
                }}
                
                CV:
                
                {cvText}
                ";
            // prepare the request to send to ai 
            var request = new
            {
                model = "deepseek/deepseek-chat-v3-0324",
                max_tokens = 1000,
                temperature = 0,
                messages = new[]
           {
                new
                {
                    role = "user",
                    content = prompt
                }
            }
            };
            // clear header for no confusion with old api key sent 
            _httpClient.DefaultRequestHeaders.Clear();
            // identify ai
            _httpClient.DefaultRequestHeaders.Authorization =
     new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);
            // get response
            Console.WriteLine("===== REQUEST =====");
            Console.WriteLine(JsonSerializer.Serialize(request));
            Console.WriteLine("===================");
            var response = await _httpClient.PostAsJsonAsync(
                "https://openrouter.ai/api/v1/chat/completions",
                request);
            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine("===== RAW RESPONSE =====");
            Console.WriteLine(result);
            Console.WriteLine("========================");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(result);
            }
            var document = JsonDocument.Parse(result);
            // deserialize AI response here
            var aiJson = document
                .RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();
            Console.WriteLine(aiJson);
            aiJson = aiJson.Trim();
            Console.WriteLine("===== AI CONTENT =====");
            Console.WriteLine(aiJson);
            Console.WriteLine("======================");
            int start = aiJson.IndexOf('{');
            int end = aiJson.LastIndexOf('}');

            if (start == -1 || end == -1)
            {
                throw new Exception("Invalid AI response: no JSON found");
            }
            
            aiJson = aiJson.Substring(start, end - start + 1);
            var matchResult =
                JsonSerializer.Deserialize<AnalysisResultDto>(
                    aiJson,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            
            if (matchResult == null)
            {
                throw new Exception("Failed to parse AI response.");
            }
            var job = new Analysis()
            {
                Score = matchResult.Score,
                SuggestedSkills = JsonSerializer.Serialize(matchResult.SuggestedSkills),
                SuggestedProjects = JsonSerializer.Serialize(matchResult.SuggestedProjects),
                Summary = matchResult.Summary,
                cv = cv,
                Strengths = JsonSerializer.Serialize(matchResult.Strengths),
                CreatedAt = DateTime.UtcNow,
                CVId = cv.Id,
                Weaknesses = JsonSerializer.Serialize(matchResult.Weaknesses)

            };
            await _db.Analyses.AddAsync(job);
            await _db.SaveChangesAsync();
            return matchResult;

        }

        public async Task<JobMatchResultDto> MatchCVWithJobAsync(string cvText, string jobDescription,CV cv)
        {
            var apiKey = Environment.GetEnvironmentVariable("OPENROUTER_API_KEY");
            Console.WriteLine("API KEY = " + apiKey);
            var prompt = $@"
                            Compare this CV with the Job Description.
                            Return ONLY valid JSON in this format:
                            {{
                              ""MatchPercentage"": number,
                              ""MatchingSkills"": [],
                              ""MissingSkills"": [],
                              ""Recommendation"": """",
                              ""Roadmap"": []
                            }}
                            
                            CV:
                            {cvText}
                            
                            JOB DESCRIPTION:
                            {jobDescription}
                            ";

            var request = new
            {
                model = "deepseek/deepseek-chat-v3-0324",
                messages = new[]
                {
            new
            {
                role = "user",
                content = prompt
            }
        }
            };

            _httpClient.DefaultRequestHeaders.Clear();

            _httpClient.DefaultRequestHeaders.Authorization =
     new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);

            var response = await _httpClient.PostAsJsonAsync(
                "https://openrouter.ai/api/v1/chat/completions",
                request);

            var result = await response.Content.ReadAsStringAsync();

            Console.WriteLine("===== RAW RESPONSE =====");
            Console.WriteLine(result);
            Console.WriteLine("========================");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(result);
            }

            using var document = JsonDocument.Parse(result);

            var aiJson = document
                .RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            Console.WriteLine(aiJson);
            
            aiJson = aiJson.Trim();
            Console.WriteLine("===== AI CONTENT =====");
            Console.WriteLine(aiJson);
            Console.WriteLine("======================");


            int start = aiJson.IndexOf('{');
            int end = aiJson.LastIndexOf('}');

            if (start == -1 || end == -1)
            {
                throw new Exception("Invalid AI response: no JSON found");
            }

            aiJson = aiJson.Substring(start, end - start + 1);
            var matchResult =
                JsonSerializer.Deserialize<JobMatchResultDto>(
                    aiJson,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            Console.WriteLine(matchResult);
            if (matchResult == null)
            {
                throw new Exception("Failed to parse AI response.");
            }
            var job = new JobMatch()
            {
                MatchingSkills = JsonSerializer.Serialize(matchResult.MatchingSkills),
                CreatedAt = DateTime.UtcNow,
                MissingSkills = JsonSerializer.Serialize(matchResult.MissingSkills),
                cv = cv,
                CVId = cv.Id,
                JobDescription = jobDescription,
                MatchPercentage = (int)matchResult.MatchPercentage

            };

            await _db.JobMatches.AddAsync(job);
            await _db.SaveChangesAsync();
            return matchResult;
        }
    }
}