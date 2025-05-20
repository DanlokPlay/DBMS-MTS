USE MTS;
GO
SELECT * FROM Client
WHERE balance > (SELECT AVG(balance) FROM Client);
