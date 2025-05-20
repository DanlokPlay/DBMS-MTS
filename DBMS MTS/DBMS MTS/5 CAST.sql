USE MTS;
GO
SELECT name, CAST(balance AS INT) AS balance_int
FROM Client;
