# Technical Description

This project aims to provide a centralized management platform for a construction company’s fleet, drivers, employees, and vehicle-related operational data. The main goal is to simplify and optimize the administrative work currently done manually, especially the verification of driving times, working hours, fuel usage, vehicle activity, and route-related anomalies.

The application is designed as a business-oriented dashboard that allows the company to import, visualize, analyze, and correct operational data coming from different sources, including CSV/XML files and potentially OBU systems used in the Belgian construction sector. Rather than being a strict employee monitoring tool, the platform focuses on data consolidation, anomaly detection, and administrative support.

## Main Objectives

The platform is built to reduce manual workload, limit human errors, and improve the reliability of data processing related to drivers and rolling stock. It centralizes key information such as driver activity, vehicle usage, route history, working time calculations, fuel-related data, and detected inconsistencies.

The system also provides the client with the possibility to review and correct imported data when needed, ensuring that errors caused by missing, incomplete, or inconsistent data can be handled before administrative processing.

## Core Features
### Data Import

The application supports manual and planned imports of operational data. The first targeted formats are CSV and XML, allowing the system to integrate structured data exported from external tools or OBU-related systems.

A technical log is included to track import status, missing data, processing errors, alerts, and other technical issues. This ensures better traceability and easier debugging when data sources are incomplete or inconsistent.

### Driver Management

The platform includes a driver management module where users can view, edit, and monitor driver-related information. Driver activity can be reconstructed and analyzed based on imported data, helping the company calculate working hours and detect unusual patterns.

### Vehicle Management

A vehicle reference module stores and displays information about the company’s rolling stock. This includes vehicle identification, base mileage, activity history, and usage tracking. The goal is to provide a clear overview of each vehicle’s operational state and historical activity.

### Business Rules Engine

A rules engine is planned to automate company-specific calculations and checks. It handles business constraints such as break times, vehicle cleaning time, overtime calculation, and other rules specific to the construction and transport sector.

This component is central to the project, as it transforms raw imported data into usable administrative information.

### Anomaly Detection

The system flags suspicious or inconsistent data, such as abnormal travel duration, missing information, incomplete activities, or route-related issues. These anomalies are highlighted in the dashboard so that the responsible employee can review and correct them if necessary.

Advanced anomaly detection, such as significant detours or inconsistencies between mileage and fuel consumption, may be added later depending on data reliability and client needs.

### Activity Reconstruction

The platform aims to reconstruct driver and vehicle activity automatically. Based on imported route and location data, the system can identify activity types such as driving, waiting, working on a construction site, or other operational states.

This feature helps transform raw tracking information into meaningful business events.

### Construction Site Management

A construction site module is planned to display site addresses and basic geozones. These geozones can be used to associate vehicle presence with specific worksites and improve the accuracy of activity reconstruction.

### Fuel and Supporting Documents

Fuel management is considered as an additional feature. The system may later integrate fuel data and supporting documents such as fuel tickets, allowing better comparison between distance travelled, fuel consumption, and vehicle activity.

## Data Sources

The project is designed to work with multiple data sources:

+ CSV imports  
+ XML imports  
+ OBU-related data  
+ Vehicle location history
+ Driver and vehicle reference data
+ Construction site information
+ Potential fuel records and supporting documents

The OBU data source is particularly important because it is mandatory in the Belgian construction sector and can provide geolocation and vehicle usage data. However, its integration may require additional analysis depending on the format, accessibility, and reliability of the exported data.

## Technical Approach

The application follows a dashboard-oriented architecture, where imported data is processed, normalized, analyzed, and displayed through dedicated management views.

**The expected workflow is:**

Import operational data from CSV, XML, or OBU-related exports.  
Validate the imported data and log technical issues.  
Normalize the data into internal driver, vehicle, activity, and route models.  
Apply business rules to calculate working hours, breaks, overtime, cleaning time, and other operational values.  
Detect anomalies or inconsistencies.  
Display results in a dashboard.  
Allow authorized users to review, correct, or validate the data.  
## Project Scope

The project is expected to evolve through collaboration with the client and the employee currently responsible for the manual administrative process. As the specific constraints of the business become clearer, the platform may be extended with additional modules, improved rule handling, advanced route analysis, and more detailed reporting features.

**Prioritized Feature Roadmap**
### Must Have
+ Manual and scheduled data import
+ CSV and XML support
+ Technical log for import status, errors, missing data, and alerts
+ Driver management
+ Vehicle management
+ Base mileage and vehicle activity tracking
+ Business rules engine for breaks, cleaning time, and overtime
+ Detection of abnormal durations and basic anomalies
### Should Have
+ Use of historical location data for calculations
+ Simple map or location display
+ Construction site address visualization
+ Basic geozone support
+ Automatic reconstruction of activities such as driving, waiting, and site presence
### Could Have
+ Fuel management
+ Supporting documents such as fuel tickets
### Would Have
+ Advanced significant detour detection
+ Comparison between mileage and fuel usage, depending on data reliability
