using Newtonsoft.Json.Linq;
using RESTUtil;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P3starter
{
    public partial class Main : Form
    {
        RESTapi rest = new RESTapi("http://ist.rit.edu/api");

        public Main()
        {
            InitializeComponent();
            Populate();
        }

        public void Populate()
        {
            
            // Get the /about/ information from the API
            string jsonAbout = rest.getRESTData("/about/");

            // Console.WriteLine(jsonAbout);

            // need to get the data out of the JSON string 
            // into an object form that we can use
            About about = JToken.Parse(jsonAbout).ToObject<About>();

            // About title
            lbl_aboutTitle.Text = about.title;
            lbl_aboutTitle.Font = new Font("Neo Sans", 9, FontStyle.Regular);
            rtb_desc.Text = about.description;
            lbl_about_quoteAuthor.Text = about.quoteAuthor;
            tb_quote.Text = about.quote;
            lbl_quote.Font = new Font("Neo Sans", 9, FontStyle.Regular);

            // get resources, a link to click on 
            string jsonRes = rest.getRESTData("/resources/");

            // Get information out of the resources object
            Resources resources = JToken.Parse(jsonRes).ToObject<Resources>();
        }



        // DEGREES TAB
        private void tab_Degrees_Enter(object sender, EventArgs e)
        {
            string jsonDegrees = rest.getRESTData("/degrees/");

            Degree degree = JToken.Parse(jsonDegrees).ToObject<Degree>();


            for (int i = 0; i < degree.undergraduate.Count; i++) {
                var label = Controls.Find("lbl_deg_" + (i + 1), true).FirstOrDefault();
                var rtb = Controls.Find("rtb_deg_" + (i + 1), true).FirstOrDefault();
                var listView = Controls.Find("deg_listView" + (i), true).FirstOrDefault();
                
                label.Text = degree.undergraduate[i].title.ToUpper();
                label.Font = new Font("Neo Sans", 9, FontStyle.Regular);
                rtb.Text = degree.undergraduate[i].description;

            } //end undergrad for loop

        
            // First degree list view
            deg_listView0.Clear();
            deg_listView0.View = View.Details;
            deg_listView0.Columns.Add("Concentrations", 200);
            // add information from the emp object
            ListViewItem item;
            for (int i = 0; i < degree.undergraduate[0].concentrations.Count; i++)
            {
                item = new ListViewItem(new string[]
                {
                    degree.undergraduate[0].concentrations[i]
                });

                // append the row to the list view
                deg_listView0.Items.Add(item);

            } // end for


            // Second degree list view
            deg_listView1.Clear();
            deg_listView1.View = View.Details;
            deg_listView1.Columns.Add("Concentrations", 200);
            // add information from the emp object
            //ListViewItem item;
            for (int i = 0; i < degree.undergraduate[1].concentrations.Count; i++)
            {
                item = new ListViewItem(new string[]
                {
                    degree.undergraduate[1].concentrations[i]
                });

                // append the row to the list view
                deg_listView1.Items.Add(item);

            } // end for


            // Third degree list view
            deg_listView2.Clear();
            deg_listView2.View = View.Details;
            deg_listView2.Columns.Add("Concentrations", 200);
            // add information from the emp object
            //ListViewItem item;
            for (int i = 0; i < degree.undergraduate[2].concentrations.Count; i++)
            {
                item = new ListViewItem(new string[]
                {
                    degree.undergraduate[2].concentrations[i]
                });

                // append the row to the list view
                deg_listView2.Items.Add(item);

            } // end for




            for (int i = 0; i < degree.graduate.Count(); i++)
            {
                var label = Controls.Find("lbl_grad_deg_" + (i + 1), true).FirstOrDefault();
                var rtb = Controls.Find("rtb_grad_deg_" + (i + 1), true).FirstOrDefault();

                if (i != degree.graduate.Count() - 1)
                {
                    label.Text = degree.graduate[i].title.ToUpper();
                    label.Font = new Font("Neo Sans", 9, FontStyle.Regular);
                    rtb.Text = degree.graduate[i].description;

                }
                else
                {
                    label.Text = degree.graduate[i].degreeName.ToUpper();
                    for (int j = 0; j < degree.graduate[i].availableCertificates.Count(); j++)
                    {
                        rtb.Text += degree.graduate[i].availableCertificates[j];
                    }
                }
            } //end grad for loop


            // First grad degree list view
            grad_deg_listView0.Clear();
            grad_deg_listView0.View = View.Details;
            grad_deg_listView0.Columns.Add("Concentrations", 200);
            // add information from the emp object
            //ListViewItem item;
            for (int i = 0; i < degree.graduate[0].concentrations.Count; i++)
            {
                item = new ListViewItem(new string[]
                {
                    degree.graduate[0].concentrations[i]
                });

                // append the row to the list view
                grad_deg_listView0.Items.Add(item);

            } // end for


            // Second grad degree list view
            grad_deg_listView1.Clear();
            grad_deg_listView1.View = View.Details;
            grad_deg_listView1.Columns.Add("Concentrations", 200);
            // add information from the emp object
            //ListViewItem item;
            for (int i = 0; i < degree.graduate[1].concentrations.Count; i++)
            {
                item = new ListViewItem(new string[]
                {
                    degree.graduate[1].concentrations[i]
                });

                // append the row to the list view
                grad_deg_listView1.Items.Add(item);

            } // end for


            // Third grad degree list view
            grad_deg_listView2.Clear();
            grad_deg_listView2.View = View.Details;
            grad_deg_listView2.Columns.Add("Concentrations", 200);
            // add information from the emp object
            //ListViewItem item;
            for (int i = 0; i < degree.graduate[2].concentrations.Count; i++)
            {
                item = new ListViewItem(new string[]
                {
                    degree.graduate[2].concentrations[i]
                });

                // append the row to the list view
                grad_deg_listView2.Items.Add(item);

            } // end for


        } // end of degree page





         /*
         * The code below is to load the Coop Table
         * Load the Employment data into an object
         * Use that object for the different loads
         */

        private Employment emp = null;

        private void loadEmploymentData()
        {

            // Have we loaded the data before
            if (emp == null)
            {

                string jsonEmp = rest.getRESTData("/employment/");

                emp = JToken.Parse(jsonEmp).ToObject<Employment>();
            }
        }

        // EMPLOYMENT TAB
        private void tab_Employment_Enter(object sender, EventArgs e)
        {
            loadEmploymentData();

            for (var i = 0; i < emp.coopTable.coopInformation.Count; i++)
            {
                // add a row to place information into it
                dataGridView1.Rows.Add();

                dataGridView1.Rows[i].Cells[0].Value = emp.coopTable.coopInformation[i].employer;

                dataGridView1.Rows[i].Cells[1].Value = emp.coopTable.coopInformation[i].degree;

                dataGridView1.Rows[i].Cells[2].Value = emp.coopTable.coopInformation[i].city;

                dataGridView1.Rows[i].Cells[3].Value = emp.coopTable.coopInformation[i].term;
            } //end for loop coop table



            for (var i = 0; i < emp.employmentTable.professionalEmploymentInformation.Count; i++)
            {
                // add a row to place information into it
                dataGridView2.Rows.Add();

                dataGridView2.Rows[i].Cells[0].Value = emp.employmentTable.professionalEmploymentInformation[i].employer;

                dataGridView2.Rows[i].Cells[1].Value = emp.employmentTable.professionalEmploymentInformation[i].degree;

                dataGridView2.Rows[i].Cells[2].Value = emp.employmentTable.professionalEmploymentInformation[i].city;

                dataGridView2.Rows[i].Cells[3].Value = emp.employmentTable.professionalEmploymentInformation[i].title;

                dataGridView2.Rows[i].Cells[4].Value = emp.employmentTable.professionalEmploymentInformation[i].startDate;
            } //end for loop employment table
        } //end of employment page



        
        private void tab_Minors_Enter(object sender, EventArgs e)
        {
            string jsonMinors = rest.getRESTData("/minors/");

            Minors minors = JToken.Parse(jsonMinors).ToObject<Minors>();


            for (int i = 0; i < minors.UgMinors.Count; i++)
            {
                var minor_btn = Controls.Find("btn_minor" + (i), true).FirstOrDefault();
                minor_btn.Text = minors.UgMinors[i].name.ToUpper();
                minor_btn.Click += new EventHandler(this.Minor_Btn_Click);
            } //end undergrad for loop

        } //end of minors page


        // Event handler for all button clicks on minors page
        private void Minor_Btn_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;

            switch (clickedButton.Name) {
                case "btn_minor0":
                    populateMinorBox(0);
                    break;

                case "btn_minor1":
                    populateMinorBox(1);
                    break;

                case "btn_minor2":
                    populateMinorBox(2);
                    break;

                case "btn_minor3":
                    populateMinorBox(3);
                    break;

                case "btn_minor4":
                    populateMinorBox(4);
                    break;

                case "btn_minor5":
                    populateMinorBox(5);
                    break;

                case "btn_minor6":
                    populateMinorBox(6);
                    break;

                case "btn_minor7":
                    populateMinorBox(7);
                    break;

                default:
                    Console.WriteLine("Made it to the end of the switch oh no!");
                    break;
            }
        }


        // populates rich text box based on which button is clicked
        private void populateMinorBox(int minorID) {

            string jsonMinors = rest.getRESTData("/minors/");

            Minors minors = JToken.Parse(jsonMinors).ToObject<Minors>();

            lbl_minor_title.Text = minors.UgMinors[minorID].title;
            rtb_minor_desc.Text = minors.UgMinors[minorID].description;
            rtb_minor_note.Text = minors.UgMinors[minorID].note;


            minor_course_listView.Clear();
            minor_course_listView.View = View.Details;
            minor_course_listView.Columns.Add("Courses", 100);
            ListViewItem item;
            for (var i = 0; i < minors.UgMinors[minorID].courses.Count; i++) {
                item = new ListViewItem(new string[]
                {
                    minors.UgMinors[minorID].courses[i]
                });
                minor_course_listView.Items.Add(item);
            }
        }


        // PEOPLE TAB
        int activeIndex = 0;
        bool isFaculty = true;
        People people;
        private void tab_People_Enter(object sender, EventArgs e)
        {
            string jsonPeople = rest.getRESTData("/people/");

            // cast the object to People
            people = JToken.Parse(jsonPeople).ToObject<People>();

            // Generic Information in People

            people_header.Text = people.title;
            lbl_people_subtitle.Text = people.subTitle;
            populatePeople(isFaculty, activeIndex);
            btn_prev_person.Enabled = false;

        }

        // when clicked switch to the faculty array list
        private void fac_list_people_Click(object sender, EventArgs e)
        {
            activeIndex = 0;
            isFaculty = true;
            populatePeople(isFaculty, activeIndex);
            btn_prev_person.Enabled = false;
            btn_next_person.Enabled = true;
        }


        // when clicked switch to the staff array list
        private void staff_list_people_Click(object sender, EventArgs e)
        {
            activeIndex = 0;
            isFaculty = false;
            populatePeople(isFaculty, activeIndex);
            btn_prev_person.Enabled = false;
            btn_next_person.Enabled = true;
        }


        // gets the previous person in the array and displays thier info
        private void btn_prev_person_Click(object sender, EventArgs e)
        {
            if (activeIndex > 0)
            {
                activeIndex -= 1;
                populatePeople(isFaculty, activeIndex);
                btn_next_person.Enabled = true;
            }
            else {
                btn_prev_person.Enabled = false;
            }
        }


        // gets the next person in the array and displays their info
        private void btn_next_person_Click(object sender, EventArgs e)
        {
            if (isFaculty)
            {
                if (activeIndex < people.faculty.Count - 1)
                {
                    activeIndex += 1;
                    populatePeople(isFaculty, activeIndex);
                    btn_prev_person.Enabled = true;
                }
                else
                {
                    btn_next_person.Enabled = false;
                }
            }
            else
            {
                if (activeIndex < people.staff.Count - 1)
                {
                    activeIndex += 1;
                    populatePeople(isFaculty, activeIndex);
                    btn_prev_person.Enabled = true;
                }
                else
                {
                    btn_next_person.Enabled = false;
                }
            }
        }


        // fills in the data of the people page based on a couple parameters
        private void populatePeople(bool isFac, int index)
        {
            if (isFac)
            {
                people_picture.Load(people.faculty[index].imagePath);
                lbl_people_name.Text = people.faculty[index].name;
                lbl_people_title.Text = people.faculty[index].title;
                lbl_people_interestArea.Text = people.faculty[index].interestArea;
                lbl_people_office.Text = people.faculty[index].office;
                lbl_people_website.Text = people.faculty[index].website;
                lbl_people_phone.Text = people.faculty[index].phone;
                lbl_people_email.Text = people.faculty[index].email;
            }
            else
            {
                people_picture.Load(people.staff[index].imagePath);
                lbl_people_name.Text = people.staff[index].name;
                lbl_people_title.Text = people.staff[index].title;
                lbl_people_interestArea.Text = people.staff[index].interestArea;
                lbl_people_office.Text = people.staff[index].office;
                lbl_people_website.Text = people.staff[index].website;
                lbl_people_phone.Text = people.staff[index].phone;
                lbl_people_email.Text = people.staff[index].email;
            }
        }
    } //end of class
} // end of namespace
