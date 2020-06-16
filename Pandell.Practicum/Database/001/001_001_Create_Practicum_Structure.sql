/**
* Filename: 001_001_Create_Practicum_Structure.sql
* Author: Gerry Routledge
* Description: Sometimes, we have SQL Scripts that can drop and re-create development database environments, including injecting seed data into tables that are required for development
*              and integration testing. These .sql files are normally added via versioning (IE: "001" being the first database version or iteration), so with CI/CD and deployments,
*              we can target different version and SQL Script folders upong continuous product development and deployments.
*
*              Although I do not use this folder within this Practicum, as I will be using Entity Framework mostly with the database migrations, but I wanted to showcase how I can
*              have either Entity Framework, a Database Folder wtih SQL Scripts, and/or both with CI/CD operations and localized development on a full stack scale
 */