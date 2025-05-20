USE MTS;
GO
SELECT phone_number FROM Client
UNION
SELECT receiver_number FROM Calls;
