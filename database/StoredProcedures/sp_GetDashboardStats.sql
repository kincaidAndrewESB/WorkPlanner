CREATE OR ALTER PROCEDURE sp_GetDashboardStats
AS
BEGIN

    SET NOCOUNT ON;

    SELECT
        COUNT(*) AS TotalTasks,

        SUM(
            CASE
                WHEN Status NOT IN ('Completed', 'Cancelled')
                THEN 1
                ELSE 0
            END
        ) AS OpenTasks,

        SUM(
            CASE
                WHEN Status = 'Completed'
                THEN 1
                ELSE 0
            END
        ) AS CompletedTasks,

        SUM(
            CASE
                WHEN DueDate < CAST(GETDATE() AS DATE)
                AND Status <> 'Completed'
                THEN 1
                ELSE 0
            END
        ) AS OverdueTasks

    FROM Tasks;
END
GO