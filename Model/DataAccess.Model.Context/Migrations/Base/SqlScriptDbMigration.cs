using System;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;

namespace DataAccess.Model.Context.Migrations.Base
{
    public abstract class SqlScriptDbMigration : DbMigration
    {
        public virtual string[] UpdateScripts { get { return new string[] { }; } }
        public virtual string[] RollbackScripts { get { return new string[] { }; } }

        protected virtual string ScriptDirectory { get { return "Scripts"; } }

        private string GetFilePath(string fileName)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ScriptDirectory, fileName);
        }

        private void RunScripts(string[] scripts)
        {
            var absolutePaths = scripts.Select(selector: GetFilePath);

            foreach (var scriptPath in absolutePaths)
            {
                Sql(File.ReadAllText(scriptPath));
            }
        }

        public abstract void UpdateSchema();
        public abstract void RollbackSchema();

        public void UpdateData()
        {
            RunScripts(UpdateScripts);
        }

        public void RollbackData()
        {
            RunScripts(RollbackScripts);
        }

        public sealed override void Up()
        {
            UpdateSchema();
            UpdateData();
        }

        public sealed override void Down()
        {
            RollbackSchema();
            RollbackData();
        }
    }
}
