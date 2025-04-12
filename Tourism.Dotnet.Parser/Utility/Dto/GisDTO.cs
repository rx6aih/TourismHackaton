using System.Text.Json.Serialization;
using Tourism.Dotnet.Parser.DAL.Entities;

namespace Tourism.Dotnet.Parser.Utility.Dto;

public class GisDTO
{
    [JsonPropertyName("meta")]
    public Meta Meta { get; set; }

    [JsonPropertyName("result")]
    public Result Result { get; set; }
}

public class Meta
{
    [JsonPropertyName("api_version")]
    public string ApiVersion { get; set; }

    [JsonPropertyName("code")]
    public int Code { get; set; }

    [JsonPropertyName("issue_date")]
    public string IssueDate { get; set; }
}

public class Result
{
    [JsonPropertyName("items")]
    public List<Item> Items { get; set; }

    [JsonPropertyName("total")]
    public int Total { get; set; }
}

public class Item
{
    [JsonPropertyName("address")]
    public Address Address { get; set; }

    [JsonPropertyName("address_name")]
    public string AddressName { get; set; }

    [JsonPropertyName("building_name")]
    public string BuildingName { get; set; }

    [JsonPropertyName("full_name")]
    public string FullName { get; set; }
    [JsonPropertyName("point")]
    public Point? Point { get; set; }
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("purpose_name")]
    public string PurposeName { get; set; }

    [JsonPropertyName("reviews")]
    public Reviews Reviews { get; set; }

    [JsonPropertyName("rubrics")]
    public List<Rubric>? Rubrics { get; set; }
    
    [JsonPropertyName("schedule")]
    public Schedule? Schedule { get; set; }
}
public class Address
{
    [JsonPropertyName("building_id")]
    public string BuildingId { get; set; }

    [JsonPropertyName("components")]
    public List<Component> Components { get; set; }

    [JsonPropertyName("postcode")]
    public string Postcode { get; set; }

    [JsonPropertyName("building_name")]
    public string BuildingName { get; set; }

    [JsonPropertyName("address_comment")]
    public string AddressComment { get; set; }
}
public class Component
{
    [JsonPropertyName("number")]
    public string Number { get; set; }

    [JsonPropertyName("street")]
    public string Street { get; set; }

    [JsonPropertyName("street_id")]
    public string StreetId { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("comment")]
    public string Comment { get; set; }
}

public class Reviews
{
    [JsonPropertyName("general_rating")]
    public double GeneralRating { get; set; }

    [JsonPropertyName("general_review_count")]
    public int GeneralReviewCount { get; set; }

    [JsonPropertyName("general_review_count_with_stars")]
    public int GeneralReviewCountWithStars { get; set; }

    [JsonPropertyName("is_reviewable")]
    public bool IsReviewable { get; set; }

    [JsonPropertyName("is_reviewable_on_flamp")]
    public bool IsReviewableOnFlamp { get; set; }

    [JsonPropertyName("items")]
    public List<ReviewItem> Items { get; set; }

    [JsonPropertyName("org_rating")]
    public double OrgRating { get; set; }

    [JsonPropertyName("org_review_count")]
    public int OrgReviewCount { get; set; }

    [JsonPropertyName("org_review_count_with_stars")]
    public int OrgReviewCountWithStars { get; set; }
}

public class ReviewItem
{
    [JsonPropertyName("is_reviewable")]
    public bool IsReviewable { get; set; }

    [JsonPropertyName("tag")]
    public string Tag { get; set; }

    [JsonPropertyName("rating")]
    public double Rating { get; set; }

    [JsonPropertyName("review_count")]
    public int ReviewCount { get; set; }
}

public class Rubric
{
    [JsonPropertyName("alias")]
    public string Alias { get; set; }

    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("kind")]
    public string Kind { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("parent_id")]
    public string ParentId { get; set; }

    [JsonPropertyName("short_id")]
    public int ShortId { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }
}