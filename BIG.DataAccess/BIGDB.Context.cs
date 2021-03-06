﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BIG.DataAccess
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using BIG.Model;
    
    public partial class BIG_DBEntities : DbContext
    {
        public BIG_DBEntities()
            : base("name=BIG_DBEntities")
        {
    	        this.Configuration.ProxyCreationEnabled = false;  
    
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Address> Addresses { get; set; }
        public DbSet<AddressType> AddressTypes { get; set; }
        public DbSet<Amphur> Amphurs { get; set; }
        public DbSet<CompanyInfo> CompanyInfoes { get; set; }
        public DbSet<CurrentImage> CurrentImages { get; set; }
        public DbSet<Deduction> Deductions { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeImage> EmployeeImages { get; set; }
        public DbSet<Equiptment> Equiptments { get; set; }
        public DbSet<FingerScan> FingerScans { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Geography> Geographies { get; set; }
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<Logon> Logons { get; set; }
        public DbSet<Marital> Maritals { get; set; }
        public DbSet<MasterConfig> MasterConfigs { get; set; }
        public DbSet<OtherDocument> OtherDocuments { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<ReferenceDocument> ReferenceDocuments { get; set; }
        public DbSet<ReferencePerson> ReferencePersons { get; set; }
        public DbSet<Site> Sites { get; set; }
        public DbSet<SSO> SSOes { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<WorkExperience> WorkExperiences { get; set; }
        public DbSet<Zipcode> Zipcodes { get; set; }
    }
}
