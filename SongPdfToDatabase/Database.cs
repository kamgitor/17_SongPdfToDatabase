
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SQLite;


public class Database
{

	static SQLiteConnection m_dbConnection;

	// *******************************************************
	static public void Create()
	{
		SQLiteConnection.CreateFile("Baza.s3db");

	}   // Create


	// *******************************************************
	// Creates a connection with our database file.
	static public void ConnectTo()
	{
		// m_dbConnection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
		m_dbConnection = new SQLiteConnection("Data Source=Baza.s3db;Version=3;");
		m_dbConnection.Open();

	}   // ConnectTo


	// *******************************************************
	// Tworzy strukture spiewnika w bazie danych
	static public void CreateTable()
	{
		// string sql = "create table Spiewnik (name varchar(20), score int)";
		string sql = "CREATE TABLE Spiewnik (Number1 TEXT, Number2 TEXT, Number3 TEXT, Title TEXT, Text TEXT, Repeat TEXT, Chords TEXT, Grupe TEXT, Note TEXT, Markers TEXT, Goto TEXT, Melody TEXT, Author TEXT, Chords2 TEXT, Key TEXT)";
		SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
		command.ExecuteNonQuery();

	}   // CreateTable

	// *******************************************************
	// Dodaje songa do bazy
	static public void CreateSong(string num1, string num2, string num3, string title, string text, string repeat, string chords, string grupe, string note, string markers, string go_to, string melody, string author, string chords2, string key)
	{
		string sql = string.Format("INSERT INTO Spiewnik (Number1, Number2, Number3, Title, Text, Repeat, Chords, Grupe, Note, Markers, Goto, Melody, Author, Chords2, Key) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}')", num1, num2, num3, title, text, repeat, chords, grupe, note, markers, go_to, melody, author, chords2, key);
		SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
		command.ExecuteNonQuery();

	}   // CreateSong

}