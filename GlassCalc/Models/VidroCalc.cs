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
        public int CaixaAr { get; set; } //holds air gap dimention in milimeters
        public bool IsLowE { get; set; } //holds if value is low emissivity
        public string Oclusao { get; set; } //holds the type of night oclusion

        public double Interpolate(double x0, double x, double x1, double y0, double y1)
        {
            return y0+(y1-y0)*(x-x0)/(x1-x0);
        }
        
        
        public double GetUw()
        {
            var vidroRepository = new VidroRepository();
            var selectedGlasses = vidroRepository.Vidros.Where(x => x.Material == this.Material)
                .Where(x => x.IsDoubleGlass == this.IsDouble)
                .Where(x => x.WinType == this.Abertura || x.WinType == "Todas")
                //.Where(x => x.AirDimension == this.CaixaAr) //need to solve problem when air gap is betwheen 6 and 16
                .Where(x => x.IsLowE == this.IsLowE) 
                .Where(x => x.OclusionType == this.Oclusao); //need to solve problem when "Nenhuma" is selected
            Vidro selectedGlass = selectedGlasses.FirstOrDefault() ;
            return selectedGlass.Id;
        }
        public double GetUwdn()
        {
            var vidroRepository = new VidroRepository();
            Vidro selectedGlass = vidroRepository.Vidros[0];
            return selectedGlass.Uwdn;
        }

        public int GetCaixaAr()
        {
            return this.CaixaAr;
        }

        public void ValidateInputs()
        {
            if (!this.IsDouble) //when glass is not double the air gap is zero and cannot be low E
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
