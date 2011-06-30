using System;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace WebApplication1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        MongoCollection<Subscriber> _subscriberCollection;

        protected void Page_Load(object sender, EventArgs e)
        {
            //string connectionString = "mongodb://localhost";
            _subscriberCollection = InitCollection();

            //get all rows of the table as a cursor
            //QueryCollection();

            //where clause
            //QueryCollection(new QueryDocument("FirstName", "John"));

            //find one does not return a cursor
            //PopulateLabel(_subscriberCollection.FindOne());

            //Insert
            //InsertCollection();
            
            //Remove
            //RemoveCollection();

        }

        private MongoCollection<Subscriber> InitCollection()
        {
            //string connectionString = "mongodb://dbh11.mongolab.com:27117/";
            //MongoServer server = MongoServer.Create(connectionString);
            //MongoDatabase db = server.GetDatabase("hmicmsm", new MongoCredentials("amey1", "somepwd"));

            //for local
            string connectionString = "mongodb://localhost";
            MongoServer server = MongoServer.Create(connectionString);
            MongoDatabase db = server.GetDatabase("MyCMS");

            
            //get the table
            return db.GetCollection<Subscriber>("Subscriber");
        }

        private void QueryCollection(QueryDocument q = null)
        {
            MongoCursor<Subscriber> subscribers;
            if (q == null)
                subscribers = _subscriberCollection.FindAll();
            else
                subscribers = _subscriberCollection.Find(q);

            PopulateRpt(subscribers);
        }

        private void InsertCollection()
        {
            lblTitle.Text = "Insert";
            Subscriber sub = new Subscriber
                                 {
                                     SubscriberId = 111,
                                     PrimaryEmailAddress = "some@test.com",
                                     FirstName = "Amey",
                                     LastName = "Test"
                                 };
            _subscriberCollection.Insert(sub);
            QueryCollection();
        }
        
        private void RemoveCollection()
        {
            lblTitle.Text = "Remove";

            QueryComplete q = Query.And(
                Query.EQ("FirstName", "Amey"),
                Query.EQ("LastName", "Test")
            );

            _subscriberCollection.Remove(q);
            //_subscriberCollection.RemoveAll();
            //_subscriberCollection.FindAndRemove();
            QueryCollection();
        }

        private void UpdateCollection()
        {
            lblTitle.Text = "Update";

            QueryComplete q = Query.And(
                Query.EQ("FirstName", "Ernest"),
                Query.EQ("LastName", "Hemingway")
            );

            var update = Update.Set("LastName", "Chow");
            _subscriberCollection.Update(q, update, UpdateFlags.Upsert);
            QueryCollection();
        }


        //private void PopulateRpt(Subscriber s)
        //{
        //    rptSubscribers.DataSource = s;
        //    rptSubscribers.DataBind();
        //}

        private void PopulateRpt(MongoCursor<Subscriber> subscribers)
        {
            rptSubscribers.DataSource = subscribers;
            rptSubscribers.DataBind();
        }

        private void PopulateLabel(Subscriber s)
        {
            if (s != null)
            {
                lblSome.Text += "SubscriberId: " + s.SubscriberId;
                lblSome.Text += " Email: " + s.PrimaryEmailAddress;
                lblSome.Text += " First Name: " + s.FirstName;
                lblSome.Text += " Last Name: " + s.LastName;

                lblSome.Text += "   ---   ";
            }
        }

        private void PopulateLabel(MongoCursor<Subscriber> subscribers)
        {
            foreach (Subscriber subscriber in subscribers)
            {
                PopulateLabel(subscriber);
            }


        }
    }

    public class Subscriber
    {
        public ObjectId _id { get; set; }
        public int SubscriberId { get; set; }
        public string PrimaryEmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}