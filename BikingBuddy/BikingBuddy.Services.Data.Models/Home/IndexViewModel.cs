﻿using BikingBuddy.Web.Models.Event;

namespace BikingBuddy.Services.Data.Models.Home;

public class IndexViewModel
{
    public IndexViewModel( )
    {
        TopEvents = new HashSet<EventMiniViewModel>();
    }

    public ICollection<EventMiniViewModel> TopEvents { get; set; } 
    
    public int UsersCount { get; set; }
    
    public int TeamsCount { get; set; }
    
    public int ActiveEventsCount { get; set; }
    
    public int AllEventsCount { get; set; }
}