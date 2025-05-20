USE MTS;
GO

-- ������� ��� �������
CREATE TABLE Tariff (
    tariff_id INT IDENTITY(1,1) PRIMARY KEY,       -- ������������� ������
    tariff_name NVARCHAR(50) NOT NULL,              -- �������� ������
    tariff_type NVARCHAR(20) CHECK(tariff_type IN ('�������������', '���������������')), -- ��� ������ (�������� ����������� �� �������� ������)
    switch_cost MONEY NOT NULL,                     -- ��������� �������� �� �����
    cost_local MONEY NOT NULL,                      -- ��������� ������ �� ������
    cost_long_distance MONEY NOT NULL,             -- ��������� ������ �������������
    cost_international MONEY NOT NULL              -- ��������� ������ �������������
);
GO

-- ������� ��� ��������
CREATE TABLE Client (
    client_id INT IDENTITY(1,1) PRIMARY KEY,       -- ������������� �������
    name NVARCHAR(100) NOT NULL,                    -- ��� �������
    client_type NVARCHAR(20) CHECK(client_type IN ('����������', '�����������')), -- ��� ������� (�������� ����������� �� �������� ������)
    balance MONEY NOT NULL DEFAULT 0,                    -- ������ ������� (�� ��������� 0)
    phone_number NVARCHAR(15) UNIQUE NOT NULL,     -- ����� �������� �������
    tariff_id INT,                                  -- ������������� ������ �������
    FOREIGN KEY (tariff_id) REFERENCES Tariff(tariff_id) -- ����� � �������
    ON DELETE SET NULL -- ���� ����� �����, ������ NULL � ���� ����� �������
);
GO

-- ������� ��� �������
CREATE TABLE Calls (
    call_id INT IDENTITY(1,1) PRIMARY KEY,         -- ������������� ������
    client_id INT,                                  -- ������������� �������
    receiver_number NVARCHAR(15) NOT NULL,          -- ����� ��������, �������� �������
    call_time DATETIME2 NOT NULL,                   -- ����� ������ (���������� DATETIME2)
    call_duration INT NOT NULL,                     -- ������������ ������ (� �������)
    call_type NVARCHAR(20) CHECK(call_type IN ('�� ������', '�������������', '�������������')), -- ��� ���������� (�������� ����������� �� �������� ������)
    call_cost MONEY NOT NULL DEFAULT 0,             -- ��������� ������ (�� ��������� 0)
    FOREIGN KEY (client_id) REFERENCES Client(client_id) -- ����� � ��������
    ON DELETE CASCADE -- ���� ������ �����, ��� ��� ������ ���� ���������
);
GO

-- ������� ��� �������� ����������� �������
CREATE TABLE Call_Detail_Request (
    request_id INT IDENTITY(1,1) PRIMARY KEY,      -- ������������� �������
    client_id INT,                                  -- ������������� �������
    start_date DATE NOT NULL,                       -- ��������� ���� ��� �����������
    end_date DATE NOT NULL,                         -- �������� ���� ��� �����������
    FOREIGN KEY (client_id) REFERENCES Client(client_id) -- ����� � ��������
    ON DELETE CASCADE -- ���� ������ �����, ������� ��� ����������� ���� ���������
);
GO