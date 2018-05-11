using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventario_con_listas_dobles
{
    public partial class Form1 : Form
    {
        public class Product
        {
            private int id;
            private string name;
            private double cost;

            public int Id { get { return id; } }
            public string Name { get { return name; } }
            public double Cost { get { return cost; } }

            public string Description { get; set; }
            public int Amount { get; set; }
            public Product Next { get; set; }
            public Product Before { get; set; }


            public Product(int id, string name, double turn, string description, int amount)
            {
                this.id = id;
                this.name = name;
                this.cost = turn;
                this.Description = description;
                this.Amount = amount;
            }

            public Product()
            {
                this.id = 0;
            }

            public Product(int id)
            {
                this.id = id;
            }

            public string shortDescription()
            {
                return "\n******* " + name + " *******\n" +
                        "Código: " + id + "\n";
            }

            public override string ToString()
            {
                return "\n******* " + name + " *******\n" +
                        "Código: " + id + "\n" +
                        "Amount: " + Amount + "\n" +
                        "cost: " + cost + "\n\n" +
                        "Descripción: " + Description;
            }
        }

        public class Inventory
        {
            public Product first;

            public string add(Product product)
            {
                if (first != null)
                {
                    if(first.Next != null && first.Id < product.Id)
                    {
                        Product temp = first;
                        while (temp.Next != null)
                        {
                            if (temp.Id < product.Id)
                                temp = temp.Next;
                            else if (temp.Id > product.Id)
                            {
                                temp.Before.Next = product;
                                product.Before = temp.Before;
                                temp.Before = product;
                                product.Next = temp;
                                return "Se agregó correctamente el producto con el id: " + product.Id;
                            }
                            else
                                return "El producto con el id: " + product.Id + " ya existe";
                        }
                        if (temp.Id < product.Id)
                        {
                            temp.Next = product;
                            product.Before = temp;
                        }
                        else if (temp.Id > product.Id)
                        {
                            temp.Before.Next = product;
                            product.Before = temp.Before;
                            temp.Before = product;
                            product.Next = temp;
                        }
                        else
                            return "El producto con el id: " + product.Id + " ya existe";
                        return "Se agregó correctamente el producto con el id: " + product.Id;
                    }
                    else
                    {
                        if(first.Id > product.Id)
                        {
                            Product temp = first;
                            first = product;
                            first.Next = temp;
                            temp.Before = first;
                        }
                        else if(first.Id < product.Id)
                        {
                            if(first.Next != null)
                            {
                                first.Next.Before = product;
                                product.Next = first.Next;
                            }
                            product.Before = first;
                            first.Next = product;
                            return "Se agregó correctamente el producto con el id: " + product.Id;
                        }
                        else
                            return "El producto con el id: " + product.Id + " ya existe";
                    }
                }
                else
                    first = product;
                return "Se agregó correctamente el producto con el id: " + product.Id;
            }

            internal string listProducts()
            {
                if (first != null)
                {
                    string productsString = first.ToString();
                    Product temp = first;
                    while (temp.Next != null)
                    {
                        temp = temp.Next;
                        productsString += temp.ToString();
                    }
                    return productsString;
                }
                else
                    return "No se ha agregado ningún producto al inventario";
            }

            internal string listInvert()
            {
                if (first != null)
                {
                    string productsString = first.ToString();
                    Product temp = first;
                    while (temp.Next != null)
                    {
                        temp = temp.Next;
                        productsString = temp.ToString() + productsString;
                    }
                    return productsString;
                }
                else
                    return "No se ha agregado ningún producto al inventario";
            }

            internal string listByShortDescription()
            {
                if (first != null)
                {
                    string productsString = first.shortDescription();
                    Product temp = first;
                    while (temp.Next != null)
                    {
                        temp = temp.Next;
                        productsString += temp.shortDescription();
                    }
                    return productsString;
                }
                else
                    return "No se ha agregado ningún producto al inventario";
            }

            internal Product search(int id)
            {
                if (first != null)
                {
                    Product temp = first;
                    do
                    {
                        if (temp.Id == id)
                            return temp;

                        temp = temp.Next;
                    } while (temp != null);
                }

                return null;
            }

            internal string deleteById(int id)
            {
                if (first != null)
                {
                    if (first.Id != id)
                    {
                        if (first.Next != null)
                        {
                            Product temp = first.Next;
                            do
                            {
                                if (temp.Id != id)
                                {
                                    temp = temp.Next;
                                }
                                else
                                {
                                    if(temp.Next != null)
                                    {
                                        temp.Before.Next = temp.Next;
                                        temp.Next.Before = temp.Before;
                                    }
                                    else
                                    {
                                        temp.Before.Next = null;
                                    }
                                    temp = null;
                                    return "Se eliminó correctamente el producto con el id: " + id;
                                }
                            } while (temp != null);
                        }

                        return "No se pudo eliminar el producto con id: " + id + " porque no existe";
                    }
                    else
                    {
                        if(first.Next != null)
                        {
                            first = first.Next;
                            first.Before = null;
                        }
                        else
                        first =  null;
                        return "Se eliminó correctamente el producto con el id: " + id;
                    }
                }
                else
                    return "No se ha agregado ningún producto al inventario, no se puede eliminar";
            }

            internal string deleteFirst()
            {
                if (first != null)
                    return deleteById(first.Id);
                else
                    return "No se ha agregado ningún producto al inventario, no se puede eliminar";
            }

            internal string deleteLast()
            {
                if (first != null)
                {
                    if (first.Next != null)
                    {
                        Product temp = first;
                        while (temp.Next != null)
                            temp = temp.Next;

                        temp.Before.Next = null;
                        temp = null;

                        return "Se eliminó correctamente el último producto";
                    }
                    else
                    {
                        first = null;
                        return "Se eliminó correctamente el último producto";
                    }
                }
                else
                    return "No se ha agregado ningún producto al inventario, no se puede eliminar";
            }
        }

        Inventory inventory;
        public Form1()
        {
            InitializeComponent();
            inventory = new Inventory();
            addProducts();
        }

        //Debuggin method
        public void addProducts()
        {
            Random a = new Random();
            for (int i = 1; i < 200; i++)
            {
                Console.WriteLine(i);
                inventory.add(new Product(a.Next(1, 20), "producto " + (i * 2), i * 2, "description " + (i * 2) + " " + (i * 2) + " " + " " + (i * 2), (i * 2)));
            }
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            txtMostrar.Text = inventory.listByShortDescription();
            //txtMostrar.Text = inventory.listProducts();
            lblEstado.Text = "Elementos listados";
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text.Trim() != "" && txtDescripcion.Text.Trim() != "")
            {
                string agregar = inventory.add(new Product(Convert.ToInt32(numCodigo.Value), txtNombre.Text, Convert.ToDouble(numCantidad.Value), txtDescripcion.Text, Convert.ToInt32(numCantidad.Value)));
                btnListar.PerformClick();
                lblEstado.Text = agregar;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            int codigo = Convert.ToInt32(numCodigo.Value);
            Product buscar = inventory.search(codigo);
            if (buscar != null)
            {
                lblEstado.Text = "Producto encontrado";
                MessageBox.Show(buscar.ToString());
            }
            else
            {
                lblEstado.Text = "Product no encontrado";
            }
        }

        private void btnEliminarCodigo_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(numCodigo.Value);
            string eliminar = inventory.deleteById(id);
            btnListar.PerformClick();
            lblEstado.Text = eliminar;
        }

        private void btnEliminarPrimero_Click(object sender, EventArgs e)
        {
            string eliminar = inventory.deleteFirst();
            btnListar.PerformClick();
            lblEstado.Text = eliminar;
        }

        private void btnEliminarUltimo_Click(object sender, EventArgs e)
        {
            string eliminar = inventory.deleteLast();
            btnListar.PerformClick();
            lblEstado.Text = eliminar;
        }

        private void btnListarInvertidos_Click(object sender, EventArgs e)
        {
            txtMostrar.Text = inventory.listInvert();
            lblEstado.Text = "Elementos listados inversamente";
        }
    }
}
