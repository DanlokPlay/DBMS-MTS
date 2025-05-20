USE MTS;
GO
SELECT call_id, call_cost,
    CASE 
        WHEN call_cost < 20 THEN '�������'
        WHEN call_cost BETWEEN 20 AND 45 THEN '�������'
        ELSE '�������'
    END AS call_category
FROM Calls;
