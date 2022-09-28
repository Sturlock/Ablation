using System.Collections.Generic;
using UnityEngine;

public class WillSturley_CV
{
    private string name = "Will Sturley";

    private string email = "sturlockholmes@gmail.com";
    private long number = 07875967150;
    private string portfolio = "www.github.com/Sturlock";

    private string bio =
        "A recent Graduate in Games Technology from University for the Creative Arts Farnham, graduating with a 1st Degree. " +
        "I have experience with Unity, exploring multiple disciplines e.g. Gameplay (Character Controllers, " +
        "AI Directors/Controllers), Networking(Netcode of GameObjects, Photon Fusion), " +
        "UI(Inventories, Main Menu & Options Menu, HUD). I have a major passion for making games, " +
        "I am hardworking,and have strong communication skills. Also I have experience in a leadership role and working to a deadline.";

    private List<Experience> work = new List<Experience>();
    private List<School> education = new List<School>();
    private Dictionary<string, string> skills;
    
    public void ActiveSkills()
    {
        skills.Add("Unity Engine", "4 Years");
        skills.Add("Unreal Engine", "1 Year");
        skills.Add("C#", "4 Years");
        skills.Add("C++", "1 Year");
        skills.Add("Unity Engine", "4 Years");
    }

    public void WorkExperience(int year)
    {
        Experience exp;
        for(int i = 2022; i < 2018; i--)
        {
            if (i == 2022)
            {
                title = "Junior Game Developer";
                duration = "March 2022 - Current";
                description =
                    "I work in the capacity of a Game Developer, working primarily in the Unity Engine. " +
                    "During my time in this role, I have gained experience working with Unity Netcode for GameObject, "         + "s" +
                    "Lobbies, Relay aid in creating a multiplayer game for Steam with a small team; " +
                    "this included augmenting the character controller and debugging physics over the network. " +
                    "I've worked on creating an in-game economy and ranking system, " +
                    "enabling the player to gain rewards from winning matches and completing challenges. " +
                    "Furthermore, I have experience integrating Steamworks, allowing for in-game Achievements, " +
                    "Cloud Saving and Micro transactions with Steam's Inventory System.";
                exp = new Experience(title, duration, description);
                work.Add(exp);
            }
            if (i == 2021)
            {
                title = "Retail Manager";
                duration = "August 2021 - Current";
                description = "Managing front and back of house, writing the rota, " +
                    "cashing up and working out weekly/monthly totals. " +
                    "As a Charity shop we take in donations, " +
                    "sign people up to gift aid where applicable, " +
                    "sort and price donations before steaming to put out on the shop floor.";
                exp = new Experience(title, duration, description);
                work.Add(exp);
            }
        }
    }

    public void Education()
    {
        School sch;
        for (int i = 2022; i < 2018; i--)
        {
            if (i == 2022)
            {
                school = "University for the Creative Arts ";
                graduated = 2022;
                degree = "BSc (Hons) Games Technology";
                awardedGrade = "First Degree";
                sch = new School(school, graduated, degree, awardedGrade);
                education.Add(sch);
            }
            if (i == 2018)
            {
                school = "Basingstoke College of Technology";
                graduated = 2018;
                degree = "Level 3 Extended Games Development";
                awardedGrade = "Merit";
                sch = new School(school, graduated, degree, awardedGrade);
                education.Add(sch);
            }
        }
    }




    public void Start()
    {
        name = "Something";
        email = "sturlockholmes@gmail.com";
        number = 07875967150;
        portfolio = "www.github.com/Sturlock";
        bio = "Oh god words";
    }

    private string school;
    private int graduated;
    private string degree;
    private string awardedGrade;

    private string title;
    private string description;
    private string duration;
}

public class Experience
{
    private string _title;
    private string _description;
    private string _duration;

    public Experience(string title, string duration, string description)
    {
        _title = title;
        _duration = duration;
        _description = description;
    }
}

public class School
{
    private string _school;
    private int _graduated;
    private string _degree;
    private string _awardedGrade;

    public School(string school, int graduated, string degree, string awardedGrade)
    {
        _school = school;
        _graduated = graduated;
        _degree = degree;
        _awardedGrade = awardedGrade;
    }
}