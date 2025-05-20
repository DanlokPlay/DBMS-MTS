USE MTS;
GO
INSERT INTO Calls (client_id, receiver_number, call_time, call_duration, call_type, call_cost)
VALUES (1, N'+79007654321', SYSDATETIME(), 5, N'По городу', 7.50);
