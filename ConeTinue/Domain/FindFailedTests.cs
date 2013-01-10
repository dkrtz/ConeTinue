using System;
using System.Linq;
using System.Xml.Linq;
using ConeTinue.Domain.CrossDomain;

namespace ConeTinue.Domain
{
	public class FindFailedTests
	{
		public static TestFailure[] FindFailures(string path)
		{
			var xdoc = XElement.Load(path);
			return xdoc.Elements("test-case")
			           .Where(x => x.Attribute("executed").Value == "True")
			           .Where(x => x.Attribute("success").Value == "False")
			           .Select(x => new TestFailure
				           {
					           TestKey = new TestKey() { FullName = x.Attribute("context").Value + "." + x.Attribute("name").Value, TestAssembly = new TestAssembly(x.Attribute("assembly").Value)},
					           File = x.Element("failure").Attribute("file").Value,
					           Line = ToInt(x.Element("failure").Attribute("line").Value),
					           Column = ToInt(x.Element("failure").Attribute("column").Value),
					           Message = x.Element("failure").Element("message").Value,
					           Context = x.Attribute("context").Value,
					           TestName = x.Attribute("name").Value
				           })
			           .ToArray();
		}

		private static int ToInt(string value)
		{
			int tmp;
			if (Int32.TryParse(value, out tmp))
				return tmp;
			return 0;
		}
	}
}