﻿using System;
using System.Collections.Generic;
using OpenDMS.Storage.Data;

namespace OpenDMS.Storage.Providers.CouchDB
{
    public class Engine : EngineBase
    {
		#region Fields (1) 

        private bool _ignoringAuthenticateComplete;

		#endregion Fields 

		#region Constructors (1) 

        public Engine()
            : base()
        {
            if (Logger.Storage != null) Logger.Storage.Debug("Instantiating engine...");
            _ignoringAuthenticateComplete = false;
            _isInitializing = false;
            if (Logger.Storage != null) Logger.Storage.Debug("Engine instantiated.");
        }

		#endregion Constructors 

		#region Methods (18) 

		// Public Methods (18) 

        public override void AuthenticateUser(EngineRequest request, string username, string hashedPassword)
        {
            CheckInitialization();
            Logger.Storage.Debug("Authenticating user: " + username);
            EngineMethods.AuthenticateUser act = new EngineMethods.AuthenticateUser(request, _sessionMgr, username, hashedPassword);
            act.Execute();
        }

        public override void  CheckoutCurrentVersion(EngineRequest request, ResourceId resource)
        {
            CheckInitialization();
            Logger.Storage.Debug("Checking out the current version of resource '" + resource.ToString() + "'");
            EngineMethods.CheckoutCurrentVersion act = new EngineMethods.CheckoutCurrentVersion(request, resource);
            act.Execute();
        }

        public override void CheckoutResource(EngineRequest request, ResourceId resource)
        {
            CheckInitialization();
            Logger.Storage.Debug("Checking out the resource '" + resource.ToString() + "'");
            EngineMethods.CheckoutResource act = new EngineMethods.CheckoutResource(request, resource);
            act.Execute();
        }

        public override void CheckoutVersion(EngineRequest request, Data.VersionId version)
        {
            CheckInitialization();
            Logger.Storage.Debug("Checking out the version '" + version.ToString() + "'");
            EngineMethods.CheckoutVersion act = new EngineMethods.CheckoutVersion(request, version);
            act.Execute();
        }

        public override void CreateGroup(EngineRequest request, Security.Group group)
        {
            CheckInitialization();
            Logger.Storage.Debug("Creating group: " + group.GroupName + "...");
            EngineMethods.CreateGroup act = new EngineMethods.CreateGroup(request, group);
            act.Execute();
        }

        public override void CreateNewResource(EngineRequest request, CreateResourceArgs args)
        {
            CheckInitialization();
            Logger.Storage.Debug("Creating new resource...");
            EngineMethods.CreateNewResource act = new EngineMethods.CreateNewResource(request, args);
            act.Execute();
        }

        public override void CreateNewVersion(EngineRequest request, CreateVersionArgs args)
        {
            CheckInitialization();
            Logger.Storage.Debug("Creating a new version for resource '" + args.VersionId.ResourceId.ToString() + "'...");
            EngineMethods.CreateNewVersion act = new EngineMethods.CreateNewVersion(request, args);
            act.Execute();
        }

        public override void CreateUser(EngineRequest request, Security.User user)
        {
            CheckInitialization();
            Logger.Storage.Debug("Creating user '" + user.Id + "'...");
            EngineMethods.CreateUser act = new EngineMethods.CreateUser(request, user);
            act.Execute();
        }

        public override void DeleteResource(EngineRequest request, Data.ResourceId resource)
        {
            CheckInitialization();
            Logger.Storage.Debug("Deleting resource '" + resource.ToString() + "'...");
            EngineMethods.DeleteResource act = new EngineMethods.DeleteResource(request, resource);
            act.Execute();
        }

        public override void DetermineIfInstalled(EngineRequest request, string logDirectory)
        {
            new OpenDMS.IO.Logger(logDirectory);
            new OpenDMS.Networking.Logger(logDirectory);
            new OpenDMS.Storage.Logger(logDirectory);
            Logger.Storage.Debug("Checking if OpenDMS.Storage has been installed on the db: " + request.Database.Name + " on server: " + request.Database.Server.Uri.ToString());
            EngineMethods.DetermineIfInstalled act = new EngineMethods.DetermineIfInstalled(request);
            act.Execute();
        }

        public override void GetAllGroups(EngineRequest request)
        {
            CheckInitialization();
            Logger.Storage.Debug("Getting all groups...");
            EngineMethods.GetAllGroups act = new EngineMethods.GetAllGroups(request);
            act.Execute();
        }

        public override void GetAllUsers(EngineRequest request)
        {
            CheckInitialization();
            Logger.Storage.Debug("Getting all users...");
            EngineMethods.GetAllUsers act = new EngineMethods.GetAllUsers(request);
            act.Execute();
        }

        public override void GetGlobalPermissions(EngineRequest request)
        {
            CheckInitialization();
            Logger.Storage.Debug("Getting global permissions...");
            EngineMethods.GetGlobalPermissions act = new EngineMethods.GetGlobalPermissions(request);
            act.Execute();
        }

        public override void GetGroup(EngineRequest request, string groupName)
        {
            CheckInitialization();
            Logger.Storage.Debug("Getting group '" + groupName + "'...");
            EngineMethods.GetGroup act = new EngineMethods.GetGroup(request, new Security.Group(groupName));
            act.Execute();
        }

        public override void GetResourceReadOnly(EngineRequest request, ResourceId resource)
        {
            CheckInitialization();
            Logger.Storage.Debug("Getting a read-only version of the resource '" + resource.ToString() + "'...");
            EngineMethods.GetResourceReadOnly act = new EngineMethods.GetResourceReadOnly(request, resource);
            act.Execute();
        }

        public override void GetResourceUsageRightsTemplate(EngineRequest request)
        {
            CheckInitialization();
            Logger.Storage.Debug("Getting resource usage rights template...");
            EngineMethods.GetResourceUsageRightsTemplate act = new EngineMethods.GetResourceUsageRightsTemplate(request);
            act.Execute();
        }

        public override void GetUser(EngineRequest request, string username)
        {
            CheckInitialization();
            Logger.Storage.Debug("Getting user '" + username + "'...");
            EngineMethods.GetUser act = new EngineMethods.GetUser(request, new Security.User(username));
            act.Execute();
        }

        public override void Initialize(string transactionRootDirectory, string logDirectory,
            List<Providers.IDatabase> databases, InitializationDelegate onInitialized)
        {
            // We do not check initialization here
            new OpenDMS.IO.Logger(logDirectory);
            new OpenDMS.Networking.Logger(logDirectory);
            new OpenDMS.Storage.Logger(logDirectory);

            OpenDMS.IO.FileSystem.Instance.Initialize(8192);
            //Transactions.Manager.Instance.Initalize(new IO.Directory(transactionRootDirectory));

            EngineRequest request = new EngineRequest();
            request.Engine = this;
            request.RequestingPartyType = Security.RequestingPartyType.System;
            EngineMethods.Initialize act = new EngineMethods.Initialize(request, _sessionMgr, 
                onInitialized, databases);
            act.Execute();
        }

        public override void Install(EngineRequest request, string logDirectory)
        {
            // Do not check initialization as it should not be initialized
            // CheckInitialization();
            if (_isInitialized) throw new InvalidOperationException("Install cannot be run on an initialized database.");

            new OpenDMS.IO.Logger(logDirectory);
            new OpenDMS.Networking.Logger(logDirectory);
            new OpenDMS.Storage.Logger(logDirectory);

            Logger.Storage.Debug("Installing to db: " + request.Database.Name + " on server: " + request.Database.Server.Uri.ToString());
            EngineMethods.Install act = new EngineMethods.Install(request);
            act.Execute();
        }

        public override void ModifyGroup(EngineRequest request, Security.Group group)
        {
            CheckInitialization();
            Logger.Storage.Debug("Modifying group '" + group.Id + "'...");
            EngineMethods.ModifyGroup act = new EngineMethods.ModifyGroup(request, group);
            act.Execute();
        }

        public override void ModifyResource(EngineRequest request, Data.Resource resource)
        {
            CheckInitialization();
            Logger.Storage.Debug("Modifying resource '" + resource.ResourceId.ToString() + "'...");
            EngineMethods.ModifyResource act = new EngineMethods.ModifyResource(request, resource);
            act.Execute();
        }

        public override void ModifyResourceUsageRightsTemplate(EngineRequest request, List<Security.UsageRight> usageRights)
        {
            CheckInitialization();
            Logger.Storage.Debug("Modifying resource usage rights template...");
            EngineMethods.ModifyResourceUsageRightsTemplate act = new EngineMethods.ModifyResourceUsageRightsTemplate(request, usageRights);
            act.Execute();
        }

        public override void ModifyUser(EngineRequest request, Security.User user)
        {
            CheckInitialization();
            Logger.Storage.Debug("Modifying user '" + user.Username + "'...");
            EngineMethods.ModifyUser act = new EngineMethods.ModifyUser(request, user);
            act.Execute();
        }

        public override void ModifyVersion(EngineRequest request, Data.Version version)
        {
            CheckInitialization();
            Logger.Storage.Debug("Modifying version '" + version.VersionId.ToString() + "'...");
            EngineMethods.ModifyVersion act = new EngineMethods.ModifyVersion(request, version);
            act.Execute();
        }

        public override void RollbackResource(EngineRequest request, ResourceId resource, int rollbackDepth)
        {
            CheckInitialization();
            Logger.Storage.Debug("Rolling back resource '" + resource.Id.ToString("N") + "' a depth of " + rollbackDepth.ToString() + "...");
            EngineMethods.RollbackResource act = new EngineMethods.RollbackResource(request, resource, rollbackDepth);
            act.Execute();
        }
        
        public override void ModifyGlobalPermissions(EngineRequest request, List<Security.UsageRight> usageRights)
        {
            CheckInitialization();
            Logger.Storage.Debug("Modifying global permissions...");
            EngineMethods.ModifyGlobalPermissions act = new EngineMethods.ModifyGlobalPermissions(request, usageRights);
            act.Execute();
        }

        public override void Search(EngineRequest request, SearchArgs args)
        {
            CheckInitialization();
            Logger.Storage.Debug("Performing search...");
            EngineMethods.Search act = new EngineMethods.Search(request, args);
            act.Execute();
        }

		#endregion Methods 
    }
}
