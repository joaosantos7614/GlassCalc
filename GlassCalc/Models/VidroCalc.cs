using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlassCalc.Models
{
    public class VidroCalc
    {
       
        public string Material { get; set; } //holds inputed material of the window
        public bool IsDouble { get; set; } //holds double or simple glass
        public string Abertura { get; set; } //holds opening type 
        public int CaixaAr { get; set; }  //holds air gap dimention in milimeters
        public bool IsLowE { get; set; } //holds if value is low emissivity
        public string Oclusao { get; set; } //holds the type of night oclusion

        public double Interpolate(double x0, double x, double x1, double y0, double y1) // method to calculate linear interpolation
        {
            return y0+(y1-y0)*(x-x0)/(x1-x0);
        }


        public double[] GetU() // the main method that will calculate an array with both values of U
        {
            this.ValidateInputs(); // validates the inputs before processing
            
            bool noOclusion = false;
            string initialOclusion = this.Oclusao; // variable that stores initial state of Oclusion property
            if (this.Oclusao == "Nenhuma") // to simplify processing, the values will always be filtered as if there is oclusion, in the end the output will be corrected
            {
                noOclusion = true;
                this.Oclusao = "Cortina";
            }
            
            double Uw = 0, Uwdn = 0;
            double[] outputArray = new double[2] { Uw, Uwdn }; //defines de array to be outputed
            //var vidroRepository = new VidroRepository();  // instantiates repository class

            if (this.CaixaAr==0|| this.CaixaAr == 6 || this.CaixaAr == 16) //branch if no interpolation is needed
            {
                outputArray = GetUArray(this.CaixaAr);
             
            }
            else //branch if interpolation is needed
            {
                double Uw1 = 0, Uwdn1 = 0, Uw2 = 0, Uwdn2 = 0;
                
                outputArray = GetUArray(6);
                Uw1 = outputArray[0];
                Uwdn1 = outputArray[1];
                
                outputArray = GetUArray(16);
                Uw2 = outputArray[0];
                Uwdn2 = outputArray[1];
                
                Uw = Interpolate(6, this.CaixaAr, 16, Uw1, Uw2);
                Uwdn = Interpolate(6, this.CaixaAr, 16, Uwdn1, Uwdn2);
                outputArray[0] = Uw;
                outputArray[1] = Uwdn;
                
            }

            if (noOclusion) 
            {
                outputArray[1] = outputArray[0]; // when no oclusion exists, Uwdn will be equal to Uw
                this.Oclusao = initialOclusion; // reverts to the initial value of oclusion
            }

            outputArray[0] = Math.Round(outputArray[0], 2); // rounds to 2 decimals before returning
            outputArray[1] = Math.Round(outputArray[1], 2);

            return outputArray;
        }
        
        public double[] GetUArray(int airGap)  // this method uses Linq to find the window for a given set of attributes
        {
            double Uw = 0, Uwdn = 0;
            double[] outputArray = new double[2] { Uw, Uwdn }; //defines the array to be outputed
            VidroRepository vidroRepository = new VidroRepository(); // instantiates repository class
            Vidro selectedGlass = vidroRepository.Vidros.Where(x => x.Material == this.Material)
                .Where(x => x.IsDoubleGlass == this.IsDouble)
                .Where(x => x.WinType == this.Abertura || x.WinType == "Todas")
                .Where(x => x.AirDimension == airGap) 
                .Where(x => x.IsLowE == this.IsLowE)
                .Where(x => x.OclusionType == this.Oclusao)
                .FirstOrDefault();

            //Vidro selectedGlass = selectedGlasses.FirstOrDefault();
            outputArray[0] = selectedGlass.Uw;
            outputArray[1] = selectedGlass.Uwdn;
            return outputArray;
        }

        
        public void ValidateInputs()  // this method is used to validade inputs, as some combinations are incompatible (should be done on the frontside)
        {
            if (!this.IsDouble) //when glass is not double, the air gap is set to zero and low E set to false
            {
                this.CaixaAr = 0;
                this.IsLowE = false;
            }
            if (this.IsLowE) //low E can only be applyed to 16mm glass
            {
                this.CaixaAr = 16;
            }

        }
    }
}
