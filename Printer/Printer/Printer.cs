﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Printer
{
    enum Status
    {
        [Description("Out of paper.")]
        OutOfPaper,
        [Description("Out of ink.")]
        OutOfInk,
        [Description("Disable")]
        Disable,
        [Description("Available")]
        Available
    }
    class Printer
    {
        private double _InkAmount = 100;　         //　インク量.
        private int    _PrintingPaperAmount = 50;  //　印刷紙の枚数.
        private int    _TotalPrintedSheets = 0;　     //　総印刷枚数.
        private string _MakerName = null;          //　メーカー名.
        private string _Model = null;              //  型番.
        public static Printer GetInstance(string makerName, string modelNumber)
        {
            if (makerName == null || modelNumber == null)
            {
                return null;
            }
            return new Printer(makerName, modelNumber);
        }
        public Printer(string makerName, string modelNumber)
        {
            MakerName = makerName;
            Model = modelNumber;
        }
        /// <summary>
        /// Get information about a printer.
        /// </summary>
        /// <returns></returns>
        public string GetInfo()
        {
            //インク量, 印刷紙の枚数, 総印刷枚数とかの情報を含んで返します。
            return string.Format("Ink Amount: {0} Printing Paper Amount: {1} Total Printed Sheets: {2}", InkAmount, PrintingPaperAmount, TotalPrintedSheets);
        }
        public override string ToString()
        {
            return string.Format("Ink Amount: {0} Printing Paper Amount: {1} Total Printed Sheets: {2}", InkAmount, PrintingPaperAmount, TotalPrintedSheets);
        }

        /// <summary>
        /// Status of a printer.
        /// </summary>
        public Status PrinterStatus  // Status of printer.
        {   
            get;
            private set;
        } = Status.Available;
        /// <summary>
        /// 印刷.
        /// </summary>
        public bool Print()
        {
            if (PrinterStatus != Status.Available) // check status of printer before printing.
            {
                return false;
            }
            InkAmount -= 3;
            PrintingPaperAmount -= 1;
            TotalPrintedSheets += 1;
            SetPrinterStatus(); // update status for printer after printed.
            return true;
        }
        /// <summary>
        /// Print many papers.
        /// </summary>
        /// <param name="amounts">Amounts of papers needs to print.</param>
        /// <returns></returns>
        public bool Prints(int amounts)
        {
            bool status = false;
            for (int i = 0; i < amounts; i++)
            {
                status = Print();
                if (!status)
                {
                    return status;
                }
            }
            return status;
        }
        /// <summary>
        /// Set the status for printer.
        /// </summary>
        private Status SetPrinterStatus()
        {
            if (InkAmount - 3 <= 0)
            {
                PrinterStatus = Status.OutOfInk;
            }
            if (PrintingPaperAmount <= 0)
            {
                PrinterStatus = Status.OutOfPaper;
            }
            return PrinterStatus;
        }
        #region Declare properties
        /// <summary>
        /// インク量.
        /// </summary>
        public double InkAmount
        {
            get
            {
                return _InkAmount;
            }
            set
            {
                _InkAmount = value;
            }
        }
        /// <summary>
        /// 印刷紙の枚数..
        /// </summary>
        public int PrintingPaperAmount
        {
            get
            {
                return _PrintingPaperAmount;
            }
            set
            {
                _PrintingPaperAmount = value;
            }
        }
        /// <summary>
        /// 総印刷枚数.
        /// </summary>
        public int TotalPrintedSheets
        {
            get
            {
                return _TotalPrintedSheets;
            }
            set
            {
                _TotalPrintedSheets = value;
            }
        }
        /// <summary>
        /// メーカー名.
        /// </summary>
        public string MakerName
        {
            get
            {
                return _MakerName;
            }
            set
            {
                _MakerName = value;
            }
        }
        /// <summary>
        /// 型番.
        /// </summary>
        public string Model
        {
            get
            {
                return _Model;
            }
            set
            {
                _Model = value;
            }
        }
        #endregion
    }
}
