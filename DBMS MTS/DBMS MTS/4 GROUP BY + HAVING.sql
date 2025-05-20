USE MTS;
GO
SELECT call_type, COUNT(*) AS call_count
FROM Calls
GROUP BY call_type
HAVING COUNT(*) >= 750;
