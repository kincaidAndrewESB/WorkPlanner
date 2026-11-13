CREATE OR ALTER PROCEDURE dbo.sp_SearchTasks
(
    @SearchTerm NVARCHAR(200) = NULL,
    @Status INT = NULL,
    @Priority INT = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        *
    FROM Tasks
    WHERE
        (
            @SearchTerm IS NULL
            OR @SearchTerm = ''
            OR Title LIKE '%' + @SearchTerm + '%'
            OR Description LIKE '%' + @SearchTerm + '%'
        )
        AND
        (
            @Status IS NULL
            OR Status = @Status
        )
        AND
        (
            @Priority IS NULL
            OR Priority = @Priority
        )
    ORDER BY DueDate;
END
GO