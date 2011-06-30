using System;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace WebApplication1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string connectionString = "mongodb://localhost";
            string connectionString = "mongodb://dbh11.mongolab.com:27117/";
            MongoServer server = MongoServer.Create(connectionString);
            MongoDatabase db = server.GetDatabase("hmicmsm", new MongoCredentials("amey1", "somepwd"));
            //MongoCredentials credentials = new MongoCredentials("username", "password");
            //MongoDatabase salaries = server.GetDatabase("salaries", credentials);

            //get the table
            MongoCollection<Subscriber> subscriberCollection = db.GetCollection<Subscriber>("Subscriber");

            //get all rows of the table as a cursor
            MongoCursor<Subscriber> subscribers = subscriberCollection.FindAll();
            PopulateLabel(subscribers);
            //where clause
            var query = new QueryDocument("FirstName", "John");
            PopulateLabel(subscriberCollection.Find(query));

            //find one does not return a cursor
            PopulateLabel(subscriberCollection.FindOne());

            //Insert
            Subscriber sub = new Subscriber
            {
                SubscriberId = 111,
                PrimaryEmailAddress = "some@test.com",
                FirstName = "Amey",
                LastName = "Test"
            };
            subscriberCollection.Insert(sub);
            PopulateLabel(subscriberCollection.FindAll());

            //QueryComplete q = Query.And(
            //    Query.EQ("FirstName", "Ernest"),
            //    Query.EQ("LastName", "Hemingway")
            //);
            
//            var update = Update.Set("LastName", "Chow");
            //subscriberCollection.Remove(q);
            //PopulateLabel(subscriberCollection.FindAll());

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