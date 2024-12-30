using MongoDB.Bson;
using MongoDB.Driver;
using UnityEngine;
using System.Threading.Tasks;

public class MongoDBManager : MonoBehaviour
{
    private MongoClient _client;
    private IMongoDatabase _database;
    private IMongoCollection<BsonDocument> _collection;

    [Header("MongoDB Settings")]
    [SerializeField] private string connectionString = "mongodb+srv://zakariaelazraq08:zaka1234@cluster0.92vex.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0"; // Your MongoDB connection string
    [SerializeField] private string databaseName = "ScoreQuiz"; // Database name
    [SerializeField] private string collectionName = "score"; // Collection name

    private void Start()
    {
        ConnectToDatabase();
    }

    /// <summary>
    /// Establishes a connection to the MongoDB database.
    /// </summary>
    private void ConnectToDatabase()
    {
        try
        {
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase(databaseName);
            _collection = _database.GetCollection<BsonDocument>(collectionName);
            Debug.Log("Connected to MongoDB!");
        }
        catch (System.Exception ex)
        {
            Debug.LogError("MongoDB Connection Error: " + ex.Message);
        }
    }

    /// <summary>
    /// Asynchronously saves a user's score to the MongoDB database.
    /// </summary>
    /// <param name="username">The username of the player.</param>
    /// <param name="score">The score of the player.</param>
    public async Task SaveScoreAsync(string username, int score)
    {
        if (_collection == null)
        {
            Debug.LogError("MongoDB collection is not initialized. Ensure the database connection is established.");
            return;
        }

        var document = new BsonDocument
        {
            { "username", username },
            { "score", score },
            { "date", System.DateTime.UtcNow }
        };

        try
        {
            await _collection.InsertOneAsync(document);
            Debug.Log("Score saved to MongoDB!");
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error saving score: " + ex.Message);
        }
    }

    /// <summary>
    /// Synchronously saves a user's score to the MongoDB database.
    /// </summary>
    /// <param name="username">The username of the player.</param>
    /// <param name="score">The score of the player.</param>
    public void SaveScore(string username, int score)
    {
        if (_collection == null)
        {
            Debug.LogError("MongoDB collection is not initialized. Ensure the database connection is established.");
            return;
        }

        var document = new BsonDocument
        {
            { "username", username },
            { "score", score },
            { "date", System.DateTime.UtcNow }
        };

        try
        {
            _collection.InsertOne(document);
            Debug.Log("Score saved to MongoDB!");
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error saving score: " + ex.Message);
        }
    }
}
