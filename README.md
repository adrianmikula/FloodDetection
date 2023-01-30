# Design

## Data

Read in via CsvHelper library (filter for last 4 hours)
Configuration via a CSV file (status - colour - algorithm - threshhold)
Stored in memory via 2 calculation numbers plus a count for each device
Avoid storing hashmaps in memory to save resource consumption


## Interface

Simple CLI to print current status/warnings for the last 4 hours for all devices
Display auto-refreshes every 30 seconds

## Algorithms

Make 2 calculations - average and maximum (4 hours) 
We don't know what the 4 hour window is until we have read all readings
(use max time from all readings)

## Class Structure


Device - name etc and latest calculations
Devices - hash map of all devices
Alerts - list of all alert
Config - all settings, list of alert types (easy to tweak configuration)
AlertType - Alert type, colour, name, threshhold
CSVReader - reads in data from the CSV files
CLI - handles user interaction and responses



