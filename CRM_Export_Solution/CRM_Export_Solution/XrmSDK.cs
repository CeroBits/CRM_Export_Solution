using System;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System.IO;
using System.ServiceModel;
using System.Xml;

namespace CRM_Export_Solution
{
    public class XrmSDK
    {
        private static CrmServiceClient _client;
        public static void Main(String SolutionCreatePath, String SolutionToExport, bool ManagedSolution, String CrmUrl, String Organization)
        {

            try
            {
                using (_client = new CrmServiceClient("Url=" + CrmUrl + Organization + "; authtype=AD; RequireNewInstance=True;"))
                {
                    try
                    {
                        Console.WriteLine("----------------------------------------------------------------------");
                        if (_client.IsReady == false)
                        {
                            Console.WriteLine("No se pudo establecer la conexion verifique la fuente");
                            return;
                        }
                        string solutionName = "";
                        string outputDir = SolutionCreatePath;

                        XmlTextReader xmlReader = new XmlTextReader(SolutionToExport);
                        while (xmlReader.Read())
                        {

                            switch (xmlReader.NodeType)
                            {
                                case XmlNodeType.Text:
                                    solutionName = xmlReader.Value;
                                    Console.WriteLine("Generando solucion..." + Environment.NewLine);
                                    ExportSolutionRequest exportedSolution = new ExportSolutionRequest();

                                    exportedSolution.Managed = ManagedSolution;
                                    exportedSolution.SolutionName = solutionName;
                                    ExportSolutionResponse exportSolutionResponse = (ExportSolutionResponse)_client.Execute(exportedSolution);

                                    byte[] exportXml = exportSolutionResponse.ExportSolutionFile;
                                    string filename = exportedSolution.SolutionName + ".zip";
                                    File.WriteAllBytes(outputDir + filename, exportXml);
                                    Console.WriteLine("Solucion exportada {0}.", outputDir + filename + Environment.NewLine);

                                    break;
                            }
                        }
                    }
                    catch (FaultException<OrganizationServiceFault> ex)
                    {
                        Console.WriteLine("Ocurrio un error: " + ex.Message);
                    }
                }
            }
            catch (FaultException<OrganizationServiceFault> ex)
            {
                string message = ex.Message;
                throw;
            }
        }

    }
}