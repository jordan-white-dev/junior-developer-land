BEGIN TRANSACTION;

CREATE TABLE unit (
	unit_id integer NOT NULL IDENTITY(1,1),
	property_id integer NOT NULL,
	tenant_id integer NULL,
	monthly_rent numeric(8, 2) NOT NULL,
	square_feet integer NOT NULL,
	number_of_beds integer NOT NULL,
	number_of_baths float NOT NULL,
	description nvarchar(max) NULL,
	tagline nvarchar(280) NULL,
	image_source nvarchar(max) NULL,
	application_fee numeric(8, 2) NOT NULL,
	security_deposit numeric(8, 2) NOT NULL,
	pet_deposit numeric(8, 2) NOT NULL,
	address_line_1 nvarchar(200) NOT NULL,
	address_line_2 nvarchar(200) NULL,
	city nvarchar(200) NOT NULL,
	us_state nvarchar(50) NOT NULL,
	zip_code integer NOT NULL,
	washer_dryer bit NULL,
	allow_cats bit NULL,
	allow_dogs bit NULL,
	parking_spots nvarchar(50) NULL,
	gym bit NULL,
	pool bit NULL,
	CONSTRAINT pk_unit_id PRIMARY KEY (unit_id),
);

CREATE TABLE property (
	property_id integer NOT NULL IDENTITY(1,1),
	owner_id integer NOT NULL,
	manager_id integer NOT NULL,
	property_name nvarchar(max) NOT NULL,
	property_type nvarchar(50) NOT NULL,
	number_of_units integer NOT NULL,
	image_source nvarchar(max) NULL,
	CONSTRAINT pk_property_id PRIMARY KEY (property_id),
);

CREATE TABLE site_user (
	user_id integer NOT NULL IDENTITY(1,1),
	first_name nvarchar(50) NOT NULL,
	last_name nvarchar(50) NOT NULL,
	phone_number nvarchar(50) NOT NULL,
	email_address nvarchar(50) NOT NULL,
	role nvarchar(50) NOT NULL,
	password nvarchar(50) NOT NULL,
	salt nvarchar(50) NOT NULL,
	CONSTRAINT pk_user_id PRIMARY KEY (user_id),
);

CREATE TABLE service_request (
	request_id integer NOT NULL IDENTITY(1,1),
	tenant_id integer NOT NULL,
	description nvarchar(max) NULL,
	is_emergency bit NULL,
	category nvarchar(50) NULL,
	is_completed bit NULL,
	CONSTRAINT pk_request_id PRIMARY KEY (request_id),
);

CREATE TABLE tenant_application (
	application_id integer NOT NULL IDENTITY(1,1),
	unit_id integer, 
	first_name nvarchar(50) NOT NULL,
	last_name nvarchar(50) NOT NULL,
	social_security_number integer NULL,
	phone_number nvarchar(50) NOT NULL,
	email_address nvarchar(50) NOT NULL,
	last_residence_owner nvarchar(50) NULL,
	last_residence_contact_phone_number nvarchar(50) NULL,
	last_residence_tenancy_start_date nvarchar(50) NULL,
	last_residence_tenancy_end_date nvarchar(50) NULL,
	employment_status bit NULL,
	employer_name nvarchar(50) NULL,
	employer_contact_phone_number nvarchar(50) NULL,
	annual_income nvarchar(50) NULL,
	number_of_residents integer NOT NULL,
	number_of_cats integer NULL,
	number_of_dogs integer NULL,
	application_approval_status bit NULL DEFAULT NULL,
	CONSTRAINT pk_application_id PRIMARY KEY (application_id),
);

CREATE TABLE payment (
	payment_id integer NOT NULL IDENTITY(1,1),
	unit_id integer NOT NULL,
	tenant_id integer NOT NULL,
	payment_amount DECIMAL(13,2) NOT NULL,
	payment_date DATE NOT NULL,
	payment_for_month integer NOT NULL,
	CONSTRAINT pk_payment_id PRIMARY KEY (payment_id),
);


SET IDENTITY_INSERT unit ON;


INSERT INTO unit (unit_id, property_id, monthly_rent, square_feet, number_of_beds, number_of_baths, description, tagline, image_source, application_fee, security_deposit, pet_deposit, address_line_1, city, us_state, zip_code, washer_dryer, allow_cats, allow_dogs, parking_spots, gym, pool) VALUES (1, 1, 900, 1000, 1, 1, 'This stylish abode is truly a delight to see. Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.', 'Charming, quaint, home.', 'https://images.pexels.com/photos/1438832/pexels-photo-1438832.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=650&w=940', 15, 450, 100, '200 Dryshore Street', 'Columbus', 'Ohio', 43210, 0, 1, 1, 'Street Parking', 0, 0);

INSERT INTO unit (unit_id, property_id, monthly_rent, square_feet, number_of_beds, number_of_baths, description, tagline, image_source, application_fee, security_deposit, pet_deposit, address_line_1, city, us_state, zip_code, washer_dryer, allow_cats, allow_dogs, parking_spots, gym, pool) VALUES (2, 1, 1200, 1300, 3, 1.5, 'You will not want to leave once you move in. Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.', 'Stylish and cozy no matter what time of year.', 'https://images.pexels.com/photos/1029599/pexels-photo-1029599.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=650&w=940', 15, 600, 100, '221 Dryshore Street', 'Columbus', 'Ohio', 43210, 1, 1, 1, 'Street Parking', 0, 0);

INSERT INTO unit (unit_id, property_id, monthly_rent, square_feet, number_of_beds, number_of_baths, description, tagline, image_source, application_fee, security_deposit, pet_deposit, address_line_1, city, us_state, zip_code, washer_dryer, allow_cats, allow_dogs, parking_spots, gym, pool) VALUES (3, 2, 1000, 1400, 2, 2, 'This overlook residence is perfect for growing families. Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.', 'The overlook is breathtaking.', 'https://images.pexels.com/photos/2063258/pexels-photo-2063258.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=650&w=940', 45, 1000, 300, '1 Cliffshore Lane', 'Columbus', 'Ohio', 43206, 1, 1, 1, '2 Car Garage', 0, 1);

INSERT INTO unit (unit_id, property_id, monthly_rent, square_feet, number_of_beds, number_of_baths, description, tagline, image_source, application_fee, security_deposit, pet_deposit, address_line_1, city, us_state, zip_code, washer_dryer, allow_cats, allow_dogs, parking_spots, gym, pool) VALUES (4, 3, 750, 800, 1, 1, 'Become a part of a true community. Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.', 'Comes fully furnished!', 'https://images.pexels.com/photos/2089698/pexels-photo-2089698.jpeg?auto=compress&cs=tinysrgb&dpr=3&h=750&w=1260', 0, 750, 200, '37 King Row', 'Columbus', 'Ohio', 43219, 0, 1, 1, 'Street Parking', 1, 0);

INSERT INTO unit (unit_id, property_id, monthly_rent, square_feet, number_of_beds, number_of_baths, description, tagline, image_source, application_fee, security_deposit, pet_deposit, address_line_1, city, us_state, zip_code, washer_dryer, allow_cats, allow_dogs, parking_spots, gym, pool) VALUES (5, 3, 850, 900, 2, 1, 'Become a part of a true community. Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.', 'Bay windows and natural lighting set this residence apart.', 'https://images.pexels.com/photos/1648838/pexels-photo-1648838.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=650&w=940', 0, 850, 200, '38 King Row', 'Columbus', 'Ohio', 43219, 1, 1, 1, 'Street Parking', 1, 0);

INSERT INTO unit (unit_id, property_id, monthly_rent, square_feet, number_of_beds, number_of_baths, description, tagline, image_source, application_fee, security_deposit, pet_deposit, address_line_1, city, us_state, zip_code, washer_dryer, allow_cats, allow_dogs, parking_spots, gym, pool) VALUES (6, 3, 950, 1000, 2, 1.5, 'Become a part of a true community. Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.', 'To sell you, all I need to do is show you.', 'https://images.pexels.com/photos/1743227/pexels-photo-1743227.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=650&w=940', 0, 950, 300, '39 King Row', 'Columbus', 'Ohio', 43219, 1, 1, 1, '1 Car Garage', 1, 0);

INSERT INTO unit (unit_id, property_id, tenant_id, monthly_rent, square_feet, number_of_beds, number_of_baths, description, tagline, image_source, application_fee, security_deposit, pet_deposit, address_line_1, city, us_state, zip_code, washer_dryer, allow_cats, allow_dogs, parking_spots, gym, pool) VALUES (7, 4, 3, 650, 800, 1, 1, 'Experience the beauty of nature every day. Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.', 'Wake up to the chirping of birds each morning.', 'https://images.pexels.com/photos/1131573/pexels-photo-1131573.jpeg?auto=compress&cs=tinysrgb&dpr=2&w=500', 0, 950, 300, '1 Outlook Road', 'Columbus', 'Ohio', 43333, 0, 0, 0, 'None', 0, 0);


SET IDENTITY_INSERT unit OFF;


SET IDENTITY_INSERT property ON;


INSERT INTO property (property_id, owner_id, manager_id, property_name, property_type, number_of_units) VALUES (1, 2, 1, 'Aquamarine Paradise', 'Duplex', 2);

INSERT INTO property (property_id, owner_id, manager_id, property_name, property_type, number_of_units) VALUES (2, 2, 1, 'Brookmeadow', 'Single Family', 1);

INSERT INTO property (property_id, owner_id, manager_id, property_name, property_type, number_of_units) VALUES (3, 2, 1, 'Kingly Estates', 'Triplex', 3);

INSERT INTO property (property_id, owner_id, manager_id, property_name, property_type, number_of_units) VALUES (4, 2, 1, 'Druid Point', 'Single Family', 1);


SET IDENTITY_INSERT property OFF;


SET IDENTITY_INSERT site_user ON;


INSERT INTO site_user (user_id, first_name, last_name, phone_number, email_address, role, password, salt) VALUES (1, 'Jason', 'Smith', '555-555-5555', 'jason@test.com', 'manager', 'password', 'salt');

INSERT INTO site_user (user_id, first_name, last_name, phone_number, email_address, role, password, salt) VALUES (2, 'Dick', 'Stevens', '666-666-6666', 'dick@test.com', 'owner', 'password', 'salt');

INSERT INTO site_user (user_id, first_name, last_name, phone_number, email_address, role, password, salt) VALUES (3, 'Cassie', 'Wayne', '777-777-7777', 'cassie@test.com', 'tenant', 'password', 'salt');

INSERT INTO site_user (user_id, first_name, last_name, phone_number, email_address, role, password, salt) VALUES (4, 'Joe', 'Drake', '888-888-8888', 'joe@test.com', 'tenant', 'password', 'salt');

INSERT INTO site_user (user_id, first_name, last_name, phone_number, email_address, role, password, salt) VALUES (5, 'Stephanie', 'Brown', '999-999-9999', 'stephanie@test.com', 'tenant', 'password', 'salt');


SET IDENTITY_INSERT site_user OFF;


SET IDENTITY_INSERT service_request ON;


INSERT INTO service_request (request_id, tenant_id, description, is_emergency, category) VALUES (1, 3, 'There is a hole in my ceiling.', 1, 'Other');

INSERT INTO service_request (request_id, tenant_id, description, is_emergency, category) VALUES (2, 4, 'Water leak in my bathroom.', 0, 'Plumbing');

INSERT INTO service_request (request_id, tenant_id, description, is_emergency, category) VALUES (3, 5, 'My power is out.', 1, 'Electrical');


SET IDENTITY_INSERT service_request OFF;


SET IDENTITY_INSERT tenant_application ON;


INSERT INTO tenant_application (application_id, unit_id, first_name, last_name, phone_number, email_address, last_residence_owner, last_residence_contact_phone_number, last_residence_tenancy_start_date, last_residence_tenancy_end_date, employment_status, employer_name, employer_contact_phone_number, annual_income, number_of_residents, number_of_cats, number_of_dogs) VALUES (1, 1, 'Bruce', 'Sheldon', '000-000-0000', 'bruce@test.com', 'Thomas Moon', '111-111-1111', 'March 30, 1999', 'April 20, 2019', 0, 'Unemployed', '222-222-2222', '18000', 2, 0, 0);

INSERT INTO tenant_application (application_id, unit_id, first_name, last_name, phone_number, email_address, last_residence_owner, last_residence_contact_phone_number, last_residence_tenancy_start_date, last_residence_tenancy_end_date, employment_status, employer_name, employer_contact_phone_number, annual_income, number_of_residents, number_of_cats, number_of_dogs) VALUES (2, 3, 'Amanda', 'Kent', '222-222-2222', 'amanda@test.com', 'Jacob Trent', '333-333-3333', 'June 4, 2011', 'April 21, 2019', 1, 'Housing Services Inc.', '444-444-4444', '60000', 3, 1, 0);

INSERT INTO tenant_application (application_id, unit_id, first_name, last_name, phone_number, email_address, last_residence_owner, last_residence_contact_phone_number, last_residence_tenancy_start_date, last_residence_tenancy_end_date, employment_status, employer_name, employer_contact_phone_number, annual_income, number_of_residents, number_of_cats, number_of_dogs) VALUES (3, 4, 'Diana', 'Gordon', '444-444-4444', 'diana@test.com', 'Buckeye Realty', '555-555-5555', 'January 23, 2018', 'April 22, 2019', 1, 'Self-Employed', '666-666-6666', '30000', 1, 1, 1);


SET IDENTITY_INSERT tenant_application OFF;


INSERT INTO payment ( unit_id, tenant_id, payment_amount, payment_date, payment_for_month) VALUES ( 7, 3, 650.00, '2019-01-27', 1 );

INSERT INTO payment ( unit_id, tenant_id, payment_amount, payment_date, payment_for_month) VALUES ( 7, 3, 550.00, '2019-02-28', 2 );

INSERT INTO payment ( unit_id, tenant_id, payment_amount, payment_date, payment_for_month) VALUES ( 7, 3, 750.00, '2019-03-25', 3 );

INSERT INTO payment ( unit_id, tenant_id, payment_amount, payment_date, payment_for_month) VALUES ( 7, 3, 650.00, '2019-05-03', 4 );

INSERT INTO payment ( unit_id, tenant_id, payment_amount, payment_date, payment_for_month) VALUES ( 7, 3, 650.00, '2019-06-01', 5 );

INSERT INTO payment ( unit_id, tenant_id, payment_amount, payment_date, payment_for_month) VALUES ( 7, 3, 650.00, '2019-06-29', 6 );


ALTER TABLE unit
ADD FOREIGN KEY (property_id)
REFERENCES property(property_id)

ALTER TABLE unit
ADD FOREIGN KEY (tenant_id)
REFERENCES site_user(user_id)

ALTER TABLE service_request
ADD FOREIGN KEY (tenant_id)
REFERENCES site_user(user_id)

ALTER TABLE tenant_application
ADD FOREIGN KEY (unit_id)
REFERENCES unit(unit_id)

ALTER TABLE payment
ADD FOREIGN KEY (unit_id)
REFERENCES unit(unit_id)

COMMIT TRANSACTION;
