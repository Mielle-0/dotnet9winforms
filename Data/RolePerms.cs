namespace it13Project.Data
{
    public static class RolePermissions
    {

        public static readonly Dictionary<string, List<string>> ReportsTabs =
            new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase)
        {
            { "System Administrator", new List<string>
                { "SentimentTrend", "TopGames", "Recommendations", "GenreSentiment", "ControversialGames", "LowConfidence" }
            },
            { "Data Analyst", new List<string>
                { "SentimentTrend", "TopGames", "Recommendations", "GenreSentiment" }
            },
            { "Marketing Manager", new List<string>
                { "TopGames", "Recommendations", "GenreSentiment" }
            },
            { "Game Developer", new List<string>
                { "TopGames", "Recommendations" }
            },
            { "Customer Support", new List<string>
                {  }
            },
            { "Stakeholder", new List<string>
                { "SentimentTrend", "TopGames", "Recommendations", "GenreSentiment", "ControversialGames" }
            }
            // Add other roles as needed
        };

        public static readonly Dictionary<string, List<string>> Permissions =
            new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase)
        {
        { "System Administrator", new List<string>
            {
                "Dashboard", "GameList",
                // "SentimentTrends",  // Temporarily disabled
                // "InfluentialReviewers",
                "Alerts", "Reports", "AdminSettings",
                "UserManagement", "ReviewPage", "SentimentPage", "ProfileSettings"
            }
        },
        { "Data Analyst", new List<string>
            {
                "Dashboard", "GameList", "ReviewPage",
                // "SentimentTrends",
                "Reports", "SentimentPage", "ProfileSettings"
            }
        },
        { "Marketing Manager", new List<string>
            {
                "Dashboard", "GameList", "GameDetails",
                // "InfluentialReviewers",
                "Alerts", "Reports", "ProfileSettings"
            }
        },
        { "Game Developer", new List<string>
            {
                "Dashboard", "GameList", "ReviewPage",
                // "SentimentTrends",
                "Reports", "ProfileSettings"
            }
        },
        { "Customer Support", new List<string>
            {
                "Dashboard", "ReviewPage", "Alerts", "ProfileSettings"
            }
        },
        { "Stakeholder", new List<string>
            {
                "Dashboard", "Reports", "GameList", "ProfileSettings"
            }
        }
        };

        public static bool HasAccess(string role, string menuKey)
        {
            if (string.IsNullOrEmpty(role)) return false;
            if (!Permissions.ContainsKey(role)) return false;

            return Permissions[role].Contains(menuKey);
        }
    }


}