<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="p.WebUI.Azure" generation="1" functional="0" release="0" Id="9a04af61-bc2c-41c7-91d1-fc0a5940bce6" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="p.WebUI.AzureGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="p.WebUI:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/p.WebUI.Azure/p.WebUI.AzureGroup/LB:p.WebUI:Endpoint1" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="p.WebUI:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/p.WebUI.Azure/p.WebUI.AzureGroup/Mapp.WebUI:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="p.WebUIInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/p.WebUI.Azure/p.WebUI.AzureGroup/Mapp.WebUIInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:p.WebUI:Endpoint1">
          <toPorts>
            <inPortMoniker name="/p.WebUI.Azure/p.WebUI.AzureGroup/p.WebUI/Endpoint1" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="Mapp.WebUI:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/p.WebUI.Azure/p.WebUI.AzureGroup/p.WebUI/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="Mapp.WebUIInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/p.WebUI.Azure/p.WebUI.AzureGroup/p.WebUIInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="p.WebUI" generation="1" functional="0" release="0" software="D:\Excite\PhotographyProject\p.WebUI.Azure\csx\Release\roles\p.WebUI" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="1792" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
            </componentports>
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;p.WebUI&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;p.WebUI&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/p.WebUI.Azure/p.WebUI.AzureGroup/p.WebUIInstances" />
            <sCSPolicyUpdateDomainMoniker name="/p.WebUI.Azure/p.WebUI.AzureGroup/p.WebUIUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/p.WebUI.Azure/p.WebUI.AzureGroup/p.WebUIFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="p.WebUIUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="p.WebUIFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="p.WebUIInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="4a010f6c-f9a1-4e78-8f5f-9496a64b43d7" ref="Microsoft.RedDog.Contract\ServiceContract\p.WebUI.AzureContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="6525acb3-08c8-4834-a0f0-6f5ca2a5ad66" ref="Microsoft.RedDog.Contract\Interface\p.WebUI:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/p.WebUI.Azure/p.WebUI.AzureGroup/p.WebUI:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>