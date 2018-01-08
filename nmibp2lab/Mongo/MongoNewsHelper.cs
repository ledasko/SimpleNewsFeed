using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using nmibp2lab.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace nmibp2lab.Mongo
{
    public class MongoNewsHelper
    {
        protected readonly MongoConnectionHandler<Post> MongoConnectionHandler;

        public MongoNewsHelper()
        {
            this.MongoConnectionHandler = new MongoConnectionHandler<Post>();
        }

        public void PostPost(Post newPosts)
        {
            this.MongoConnectionHandler.MongoCollection.InsertOne(newPosts);
        }

        public void PostComment(ObjectId id, string comment)
        {
            var filter = Builders<Post>.Filter.Eq("Id", id);
            var update = Builders<Post>.Update.Push("comments", comment);
            this.MongoConnectionHandler.MongoCollection.UpdateOne(filter, update);
        }

        public List<Post> GetNews(int number)
        {
            var sorter = Builders<Post>.Sort.Descending("_id");
            return this.MongoConnectionHandler.MongoCollection.Find(x => true).Sort(sorter).Limit(number).ToList();
        }
    }
}