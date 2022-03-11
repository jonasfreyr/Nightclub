using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

class JobCandidate
{
    public string Name { get; set; }
    public string Profession { get; set; }
    public int Age { get; set; }
    public string Description { get; set; }
    public string Hobbies { get; set; }
    public List<PreviousJob> PreviousJobs { get; set; }
    public List<Recommendation> Recommendations { get; set; }
}

class PreviousJob
{
    public string Workplace { get; set; }
    public string Profession { get; set; }

    public override string ToString() => $"{Profession} - {Workplace}";
}

class Recommendation
{
    public string Recommender { get; set; }
    public string Text { get; set; }
    
    public override string ToString() => $"{Recommender} - {Text}";
}

public class HireMenu : MonoBehaviour
{
    private List<JobCandidate> _candidates = new List<JobCandidate>()
    {
        new JobCandidate
        {
            Name = "Mark",
            Profession = "Bartender",
            Age = 32,
            Description = "I want to become a bartender",
            Hobbies = "I love bird watching",
            PreviousJobs = new List<PreviousJob>
            {
                new PreviousJob
                {
                    Workplace = "BirdLife International",
                    Profession = "Birdwatching specialist"
                },
                new PreviousJob
                {
                    Workplace = "Taxi corporation inc",
                    Profession = "Taxi driver"
                }
            },
            Recommendations = new List<Recommendation>
            {
                new Recommendation
                {
                    Recommender = "Marks dad",
                    Text = "Mark was always a good boy. Always did what I told him to do. He will be a great employee."
                }
            }
        },
        new JobCandidate
        {
            Name = "John",
            Profession = "Janitor",
            Age = 40,
            Description = "",
            Hobbies = "",
            PreviousJobs = new List<PreviousJob>
            {
                new PreviousJob
                {
                    Workplace = "BirdLife International",
                    Profession = "Birdwatching specialist"
                },
                new PreviousJob
                {
                    Workplace = "Taxi corporation inc",
                    Profession = "Taxi driver"
                }
            },
            Recommendations = new List<Recommendation>
            {
                new Recommendation
                {
                    Recommender = "Marks dad",
                    Text = "Mark was always a good boy. Always did what I told him to do. He will be a great employee."
                }
            }
        }
    };

    public Transform candidateListItemPrefab;

    [Header("Candidate list objects")] 
    public GameObject candidateListPage;
    public GameObject candidateList;

    [Header("Candidate detail objects")] 
    public GameObject candidateDetailsPage;
    public GameObject profilePic;
    public TextMeshProUGUI name;
    public TextMeshProUGUI profession;
    public TextMeshProUGUI age;
    public TextMeshProUGUI description;
    public TextMeshProUGUI hobbies;
    public TextMeshProUGUI previousJobs;
    public TextMeshProUGUI recommendations;

    private void Awake()
    {
        _renderCandidateList(_candidates);
    }

    public void BackToCandidateList()
    {
        candidateDetailsPage.SetActive(false);
        candidateListPage.SetActive(true);
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }

    private void _renderCandidateList(List<JobCandidate> candidates)
    {
        foreach (var (candidate, i) in candidates.Select((v, i) => (v, i)))
        {
            _addCandidateListItem(candidate, i);
        }
    }

    private void _addCandidateListItem(JobCandidate candidate, int listPosition)
    {
        var item = Instantiate(candidateListItemPrefab, candidateList.transform);
        item.Find("Name").GetComponent<TextMeshProUGUI>().text = candidate.Name;
        item.Find("Profession").GetComponent<TextMeshProUGUI>().text = candidate.Profession;
        item.Find("ViewButton").GetComponent<Button>().onClick.AddListener(() => _openCandidateDetails(candidate));

        var itemPositionY = -25.0f - (46.0f * listPosition);
        item.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, itemPositionY);
    }
    
    private void _openCandidateDetails(JobCandidate candidate)
    {
        _updateCandidateDetails(candidate);
        candidateListPage.SetActive(false);
        candidateDetailsPage.SetActive(true);
    }

    private void _updateCandidateDetails(JobCandidate candidate)
    {
        name.text = $"Name: {candidate.Name}";
        profession.text = $"Profession: {candidate.Profession}";
        age.text = $"Age: {candidate.Age}";
        description.text = candidate.Description;
        hobbies.text = candidate.Hobbies;
        previousJobs.text = string.Join(Environment.NewLine, candidate.PreviousJobs.Select(x => x.ToString()));
        recommendations.text = string.Join(Environment.NewLine, candidate.Recommendations.Select(x => x.ToString()));
    }
    
    
}