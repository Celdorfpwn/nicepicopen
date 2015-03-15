param($installPath, $toolsPath, $package, $project)

function applyTransform($xmlPath) {
	if (!(Test-Path $xmlPath)) {
		return;
	}

	$xml = new-object System.Xml.XmlDocument;
	$xml.Load($xmlPath);

	if (!$xml."configuration"."system.web.webPages.razor"."pages") {
		return;
	}

	if (!$xml."configuration"."system.web.webPages.razor"."pages"."namespaces") {
		$node = $xml.CreateElement("namespaces");
		$xml."configuration"."system.web.webPages.razor"."pages".AppendChild($node);
	}

	$node = $xml.CreateElement("add");
	$attribute = $xml.CreateAttribute("namespace");
	$attribute.Value = "Arraybracket.TypedViews";
	$node.Attributes.Append($attribute);
	$xml."configuration"."system.web.webPages.razor"."pages"."namespaces".AppendChild($node);
	
	$xml."configuration"."system.web.webPages.razor"."pages".pageBaseType = "Arraybracket.TypedViews.TypedViewPage";
	$xml.Save($xmlPath);
}

$projectPath = [System.IO.Path]::GetDirectoryName($project.Filename);
applyTransform("$projectPath\web.config");
applyTransform("$projectPath\Views\web.config");
