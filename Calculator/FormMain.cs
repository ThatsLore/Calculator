﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class FormMain : Form
    {

        public enum SymbolType
        {
            Number,
            Operator,
            DecimalPoint,
            PlusMinusSign,
            BackSpace,
            ClearAll,
            ClearEntry,
            Undefined
        }

        public struct BtnStruct
        {
            public char Content;
            public SymbolType Type;
            public bool IsBold;
            public BtnStruct(char c, SymbolType t = SymbolType.Undefined, bool b = false)
            {
                this.Content = c;              
                this.Type = t;
                this.IsBold = b;
            }
        }

        private BtnStruct[,] buttons =
        {
            { new BtnStruct('%'), new BtnStruct('\u0152', SymbolType.ClearEntry), new BtnStruct('C', SymbolType.ClearAll), new BtnStruct('\u232B', SymbolType.BackSpace) }, 
            { new BtnStruct('\u215F'), new BtnStruct('\u00B2'), new BtnStruct('\u221A'), new BtnStruct('\u00F7') }, 
            { new BtnStruct('7', SymbolType.Number, true), new BtnStruct('8', SymbolType.Number, true), new BtnStruct('9', SymbolType.Number, true), new BtnStruct('\u00D7', SymbolType.Operator) }, 
            { new BtnStruct('4', SymbolType.Number, true), new BtnStruct('5', SymbolType.Number, true), new BtnStruct('6', SymbolType.Number, true), new BtnStruct('-', SymbolType.Operator) }, 
            { new BtnStruct('1', SymbolType.Number, true), new BtnStruct('2', SymbolType.Number, true), new BtnStruct('3', SymbolType.Number, true), new BtnStruct('+', SymbolType.Operator) }, 
            { new BtnStruct('\u00B1', SymbolType.PlusMinusSign), new BtnStruct('0', SymbolType.Number, true), new BtnStruct(',', SymbolType.DecimalPoint), new BtnStruct('=', SymbolType.Operator) },
        };

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            MakeButtons(buttons.GetLength(0), buttons.GetLength(1));
        }

        private void MakeButtons(int rows, int cols)
        {
            int btnWidth = 80;
            int btnHeight = 60;
            int posX = 0;
            int posY = 116;
            for(int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Button myButton = new Button();
                    FontStyle fs = buttons[i, j].IsBold ? FontStyle.Bold : FontStyle.Regular;
                    myButton.Font = new Font("Segoe UI", 16, fs);
                    myButton.BackColor = buttons[i, j].IsBold ? Color.White : Color.LightGray;
                    myButton.Text = buttons[i, j].Content.ToString();
                    myButton.Width = btnWidth;
                    myButton.Height = btnHeight;
                    myButton.Top = posY;
                    myButton.Left = posX;
                    myButton.Tag = buttons[i, j];
                    myButton.Click += Button_Click;
                    this.Controls.Add(myButton);
                    posX += myButton.Width;
                }
                posX = 0;
                posY += btnHeight;
            }
            
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            BtnStruct clickedButtonStruct = (BtnStruct)clickedButton.Tag;

            switch(clickedButtonStruct.Type)
            {
                case SymbolType.Number:
                    if (lblResult.Text == "0") lblResult.Text = "";
                    lblResult.Text += clickedButton.Text;
                    break;

                case SymbolType.Operator:
                    break;

                case SymbolType.DecimalPoint:
                    if (lblResult.Text.IndexOf(",") == -1)
                        lblResult.Text += clickedButton.Text;
                    break;

                case SymbolType.PlusMinusSign:
                    if(lblResult.Text != "0")
                    {
                        if((lblResult.Text.IndexOf("-") == -1))
                        {
                            lblResult.Text = "-" + lblResult.Text;
                        }
                        else
                        {
                            lblResult.Text = lblResult.Text.Substring(1);
                        }
                    }
                    break;

                case SymbolType.BackSpace:
                    lblResult.Text = lblResult.Text.Substring(0, lblResult.Text.Length - 1);
                    if(lblResult.Text.Length == 0 || lblResult.Text == "-0")
                    {
                        lblResult.Text = "0";
                    }
                    break;

                case SymbolType.ClearAll:
                    break;

                case SymbolType.ClearEntry:
                    break;

                case SymbolType.Undefined:
                    break;

                default:
                    break;

            }


            
        }

        private void lblResult_TextChanged(object sender, EventArgs e)
        {
            if(lblResult.Text.Length > 16)
            lblResult.Text = lblResult.Text.Substring(0, 16);
            if (lblResult.Text.Length > 11)
            {
                int delta = lblResult.Text.Length - 11;
                lblResult.Font = new Font("Segoe UI", 36 - delta * (float)2.8, FontStyle.Regular);
            }
            else
            {
                lblResult.Font = new Font("Segoe UI", 36 - lblResult.Text.Length, FontStyle.Regular);
            }
        }
    }
}
