# Car Rental System

## **Development**

- To run the database separately (without starting the program) use the following command:
    ```shell
    docker-compose up sqlserver
    ```

- The program ensures the database is created and migrated automatically. But if you want to migrate it manually run the
  database separately first. The right connection string can be found in the `appsettings.json` file.

## **Introduction**

The Car Rental web application helps users for renting vehicles. The application will include three main pages: Sign Up,
Sign In, and the Main Page. These pages will be developed using ASP .NET Core to ensure efficient functionality and an
engaging user interface.

## **Scope**

The system will cover the small part of car rental like user sign up, sign and car search to booking.

## **User Roles**

1. *Customer*: Users looking to rent cars.
2. *Admin*: System administrators responsible for managing inventory, reservations, and users. it’s additional here when
   you have free time just mention it here to have good clue about the system

## **Features**

### **User Sign Up Page**

#### **Objective:**

The Sign-Up page allows users to create new accounts for accessing car rental services.

#### **Key Features:**

*User Registration Form*: Collect user information including the following fields:

1. **First Name:** [Text Field] (Required)
2. **Last Name:** [Text Field] (Required)
3. **Email Address:** [Email Field] (Required, Unique)
4. **Password:** [Password Field] (Required, with strength indicator)
5. **Confirm Password:** [Password Field] (Required, to confirm the chosen password)
6. **Phone Number:** [Phone Field] (Required)
7. **Date of Birth:** [Date Picker] (Optional, for age verification)
8. **Address Line 1:** [Text Field] (Required)
9. **Address Line 2:** [Text Field] (Optional)
10. **City:** [Text Field] (Required)
11. **Country:** [Drop-down/Select Field] (Required)
12. **Driver's License Number:** [Text Field] (Required, for car rental verification)

*Error Handling and Validation*: Display clear error messages for incomplete or incorrect form submissions to guide
users in correcting their information.

*Unique Email Check*: Ensure that each email address is unique to prevent duplicate accounts.

*User-friendly UI/UX:* Design the signup process to be visually appealing and easy to navigate and provide helpful hints
or tool-tips for users filling out the form.

### **User Sign In Page**

#### **Objective:**

The Sign-In Page of the Car Rental System Web Application serves as the entry point for registered users
to access their accounts and manage their car rental activities.

#### **Key Features:**

*User Credentials:* Users can enter their registered email address and password to access their accounts.

*Error Handling:* Display clear and user-friendly error messages for incorrect login attempts, guiding users to rectify
their input.

*Session Management:* Manage user sessions securely, including session timeouts and the ability to terminate sessions.
*Password Recovery:* Include a "**Forgot Password?**" link that allows users to initiate the password reset process and
regain access to their account. it’s additional here when you have free time check free email services in order to
generate encrypted link and email end-user with link …etc

### **User Main Page**

#### **Objective:**

Design an inviting and informative main page that is shown after users sign in to allow users to see
available cars and manage rentals one

#### **Key Features:**

*User Dashboard:* Upon logging in, showing all available cars for the rental and quick access side menu for their
reservations, booking history, and profile settings. it’s additional here when you have free time to manage reservations
and booking history

*Car Search and Reservation:* A prominent search bar allows users to easily search for available cars based on location,
dates, and preferences. Users can initiate new reservations or modify existing ones from this section. it’s additional
here when you have free time to manage reservations only w need the search

*Profile and Account Settings:* Offer an accessible area where users can update their personal information that provided
in sign up page