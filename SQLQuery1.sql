CREATE TABLE Passenger_Details (
    ID int NOT NULL,
    FName Varchar(50),
	LName Varchar(50) NOT NULL,
	Age int,
	P_Address Varchar(255),
	Mobile Varchar(50) NOT NULL,
	Email Varchar(50),
	Primary key(ID)
);

CREATE TABLE Train_Detail (
	Train_Detail_ID int NOT NULL,
    Train_Number int NOT NULL,
    Source Varchar(50) NOT NULL,
	Destination Varchar(50) NOT NULL,
	Date_Of_Travel Date Not Null,
	Train_Name Varchar(50),
	Fare float NOT NULL,
	Total_Seats_Available int,
	Vacant_Seats int,
	Primary key(Train_Detail_ID)
);



CREATE TABLE Booking (
    Booking_ID int NOT NULL,
    Total_Fare float,
	FK_Train_Detail_ID int,
	Primary key(Booking_ID),
	FOREIGN KEY (FK_Train_Detail_ID) REFERENCES Train_Detail(Train_Detail_ID)
);

Create Table Passenger_Booking (
	FK_Booking_ID int,
	FK_ID int,
	Foreign key (FK_Booking_ID) REFERENCES Booking (Booking_ID),
	Foreign key (FK_ID) REFERENCES Passenger_Details (ID)
);

Create Table Payment(
	Payment_ID int,
	Status varchar(50),
	FK_Booking_ID int,
	Primary key(Payment_ID),
	Foreign key (FK_Booking_ID) REFERENCES Booking (Booking_ID)
);
