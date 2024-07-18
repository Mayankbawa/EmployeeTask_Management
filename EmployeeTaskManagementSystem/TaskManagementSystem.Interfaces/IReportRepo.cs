﻿using TaskManagementSystem.Models;

namespace TaskManagementSystem.Interfaces
{
    public interface IReportRepo
    {
        Task<List<TaskReportVM>> GetTaskCompletionReport(DateTime startDate, DateTime endDate);
    }
}