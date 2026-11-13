CREATE OR ALTER PROCEDURE sp_GetOverdueTasks
AS
BEGIN

    SELECT *
    FROM Tasks
    WHERE DueDate < CAST(GETDATE() AS DATE)
      AND Status <> 'Completed'

    ORDER BY DueDate;
END
GO