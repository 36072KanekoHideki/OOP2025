using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class PostalCodeInfo {
    public string Prefecture { get; set; }
    public string City { get; set; }
    public string Town { get; set; }
}

public class PostalCodeResponse {
    public PostalCodeInfo Data { get; set; }
}

public class PostalCodeService {
    private static readonly HttpClient client = new HttpClient();

    public async Task<PostalCodeInfo> GetAddressFromPostalCodeAsync(string postalCode) {
        string url = $"https://jp-postal-code-api.ttskch.com/api/v1/{postalCode}.json";

        HttpResponseMessage response = await client.GetAsync(url);
        if (response.IsSuccessStatusCode) {
            string json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<PostalCodeResponse>(json);
            return result?.Data;
        }
        return null;
    }
}