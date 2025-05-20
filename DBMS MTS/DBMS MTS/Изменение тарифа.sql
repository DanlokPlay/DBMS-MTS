USE MTS;
GO
UPDATE Client
SET tariff_id = 5
FROM Client c
JOIN Tariff t ON t.tariff_id = 5
WHERE c.client_id = 21
  AND (
    (c.client_type = N'Физическое' AND t.tariff_type = N'Некорпоративный')
    OR
    (c.client_type = N'Юридическое' AND t.tariff_type = N'Корпоративный')
  );
	