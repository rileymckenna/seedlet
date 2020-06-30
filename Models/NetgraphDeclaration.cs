// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NetgraphDeclaration.cs" company="Microsoft">
//   All rights reserved
// </copyright>
// <summary>
//   Defines the ExternalNGD type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CSVToSeed.Models
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(ElementName = "ExternalNGD")]
    public class ExternalNGD
    {
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "Path")]
        public string Path { get; set; }
    }

    [XmlRoot(ElementName = "ExternalNGDs")]
    public class ExternalNGDs
    {
        [XmlElement(ElementName = "ExternalNGD")]
        public List<ExternalNGD> ExternalNGD { get; set; }
    }

    [XmlRoot(ElementName = "Config")]
    public class Config
    {
        [XmlElement(ElementName = "ExternalNGDs")]
        public ExternalNGDs ExternalNGDs { get; set; }
        [XmlAttribute(AttributeName = "DLGOutFile")]
        public string DLGOutFile { get; set; }
        [XmlAttribute(AttributeName = "PNGOutFile")]
        public string PNGOutFile { get; set; }
        [XmlAttribute(AttributeName = "DPGOutFile")]
        public string DPGOutFile { get; set; }
        [XmlAttribute(AttributeName = "CPGOutFile")]
        public string CPGOutFile { get; set; }
        [XmlAttribute(AttributeName = "DMIOutFile")]
        public string DMIOutFile { get; set; }
        [XmlAttribute(AttributeName = "SliceBase")]
        public string SliceBase { get; set; }
        [XmlAttribute(AttributeName = "DeviceSliceOutFile")]
        public string DeviceSliceOutFile { get; set; }
        [XmlAttribute(AttributeName = "PrettyPrint")]
        public string PrettyPrint { get; set; }
        [XmlAttribute(AttributeName = "BuildMetadataOutFile")]
        public string BuildMetadataOutFile { get; set; }
    }

    [XmlRoot(ElementName = "Execute")]
    public class Execute
    {
        [XmlAttribute(AttributeName = "Plugin")]
        public string Plugin { get; set; }
        [XmlAttribute(AttributeName = "Block")]
        public string Block { get; set; }
    }

    [XmlRoot(ElementName = "ExecutionOrder")]
    public class ExecutionOrder
    {
        [XmlElement(ElementName = "Execute")]
        public List<Execute> Execute { get; set; }
    }

    [XmlRoot(ElementName = "DeviceQuery")]
    public class DeviceQuery
    {
        [XmlAttribute(AttributeName = "Regex")]
        public string Regex { get; set; }
    }

    [XmlRoot(ElementName = "DownStreamDeviceQuery")]
    public class DownStreamDeviceQuery
    {
        [XmlAttribute(AttributeName = "Regex")]
        public string Regex { get; set; }
    }

    [XmlRoot(ElementName = "P2PAddressDeclaration")]
    public class P2PAddressDeclaration
    {
        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "Summarization")]
        public string Summarization { get; set; }
        [XmlElement(ElementName = "DownStreamSummaryLength")]
        public string DownStreamSummaryLength { get; set; }
        [XmlElement(ElementName = "DeviceQuery")]
        public DeviceQuery DeviceQuery { get; set; }
        [XmlElement(ElementName = "DownStreamDeviceQuery")]
        public DownStreamDeviceQuery DownStreamDeviceQuery { get; set; }
        [XmlElement(ElementName = "AddressSpace")]
        public string AddressSpace { get; set; }
    }

    [XmlRoot(ElementName = "Block")]
    public class Block
    {
        [XmlElement(ElementName = "P2PAddressDeclaration")]
        public P2PAddressDeclaration P2PAddressDeclaration { get; set; }
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "Declaration")]
        public Declaration Declaration { get; set; }
        [XmlElement(ElementName = "DFTemplate")]
        public DFTemplate DFTemplate { get; set; }
        [XmlElement(ElementName = "RowDeclaration")]
        public RowDeclaration RowDeclaration { get; set; }
        [XmlElement(ElementName = "PolicyTemplateDeclaration")]
        public PolicyTemplateDeclaration PolicyTemplateDeclaration { get; set; }
    }

    [XmlRoot(ElementName = "Declaration")]
    public class Declaration
    {
        [XmlAttribute(AttributeName = "type", Namespace = "http://www.w3.org/2001/XMLSchema-instance")]
        public string Type { get; set; }
        [XmlAttribute(AttributeName = "DefaultDcCode")]
        public string DefaultDcCode { get; set; }
    }

    [XmlRoot(ElementName = "FabricWiring")]
    public class FabricWiring
    {
        [XmlElement(ElementName = "Start")]
        public Start Start { get; set; }
        [XmlElement(ElementName = "End")]
        public End End { get; set; }
        [XmlElement(ElementName = "DevicePattern")]
        public DevicePattern DevicePattern { get; set; }
        [XmlElement(ElementName = "GroupPattern")]
        public GroupPattern GroupPattern { get; set; }
    }

    [XmlRoot(ElementName = "GroupPattern")]
    public class GroupPattern
    {
        [XmlAttribute(AttributeName = "Type")]
        public string Type { get; set; }
        [XmlAttribute(AttributeName = "StartGroupBy")]
        public string StartGroupBy { get; set; }
        [XmlAttribute(AttributeName = "EndGroupBy")]
        public string EndGroupBy { get; set; }
    }

    [XmlRoot(ElementName = "DFTemplate")]
    public class DFTemplate
    {
        [XmlElement(ElementName = "FabricWiring")]
        public List<FabricWiring> FabricWiring { get; set; }
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }
    }

    [XmlRoot(ElementName = "Row")]
    public class Row
    {
        [XmlAttribute(AttributeName = "DCName")]
        public string DCName { get; set; }
        [XmlAttribute(AttributeName = "AzSet")]
        public string AzSet { get; set; }
        [XmlAttribute(AttributeName = "Colo")]
        public string Colo { get; set; }
        [XmlAttribute(AttributeName = "Index")]
        public string Index { get; set; }
        [XmlAttribute(AttributeName = "Template")]
        public string Template { get; set; }
        [XmlAttribute(AttributeName = "Tag")]
        public string Tag { get; set; }
        [XmlAttribute(AttributeName = "Cluster")]
        public string Cluster { get; set; }
        [XmlAttribute(AttributeName = "Policy")]
        public string Policy { get; set; }
        [XmlAttribute(AttributeName = "Version")]
        public string Version { get; set; }
        [XmlAttribute(AttributeName = "Date")]
        public string Date { get; set; }
        [XmlAttribute(AttributeName = "Operation")]
        public string Operation { get; set; }
    }

    [XmlRoot(ElementName = "RowDeclaration")]
    public class RowDeclaration
    {
        [XmlElement(ElementName = "Row")]
        public List<Row> Row { get; set; }
    }

    [XmlRoot(ElementName = "Condition")]
    public class Condition
    {
        [XmlElement(ElementName = "DeploymentType")]
        public string DeploymentType { get; set; }
        [XmlElement(ElementName = "Role")]
        public string Role { get; set; }
    }

    [XmlRoot(ElementName = "Attribute")]
    public class Attribute
    {
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "Values")]
        public string Values { get; set; }
    }

    [XmlRoot(ElementName = "Attributes")]
    public class Attributes
    {
        [XmlElement(ElementName = "Attribute")]
        public Attribute Attribute { get; set; }
    }

    [XmlRoot(ElementName = "PolicyTemplate")]
    public class PolicyTemplate
    {
        [XmlElement(ElementName = "Condition")]
        public Condition Condition { get; set; }
        [XmlElement(ElementName = "Attributes")]
        public Attributes Attributes { get; set; }
    }

    [XmlRoot(ElementName = "PolicyTemplateDeclaration")]
    public class PolicyTemplateDeclaration
    {
        [XmlElement(ElementName = "PolicyTemplate")]
        public PolicyTemplate PolicyTemplate { get; set; }
    }

    [XmlRoot(ElementName = "Blocks")]
    public class Blocks
    {
        [XmlElement(ElementName = "Block")]
        public List<Block> Block { get; set; }
    }

    [XmlRoot(ElementName = "GraphDeclaration")]
    public class GraphDeclaration
    {
        [XmlElement(ElementName = "Config")]
        public Config Config { get; set; }
        [XmlElement(ElementName = "ExecutionOrder")]
        public ExecutionOrder ExecutionOrder { get; set; }
        [XmlElement(ElementName = "Blocks")]
        public Blocks Blocks { get; set; }
        [XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xsd { get; set; }
        [XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xsi { get; set; }
    }

}
