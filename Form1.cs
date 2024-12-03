//Project Semester CIS 022
//Group Members: Arjeevan Samra & Luke Williams
using IF.Lastfm.Core.Api;
using IF.Lastfm.Core.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;

namespace testsagabg
{
    public partial class Form1 : Form
    {
        private LastfmClient _client;
        public Form1()
        {
            InitializeComponent();
            _client = new LastfmClient("95331ac160a246b0ef593ce69211aa19", "50df70d0862af9013bc0a1d2920f735b");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            string artistName = txtArtistName.Text;
            if (string.IsNullOrEmpty(artistName))
            {
                MessageBox.Show("Please enter an artist name.");
                return;
            }

            try
            {
                var response = await _client.Artist.GetInfoAsync(artistName); // Corrected variable name
                if (response.Success)
                {
                    LastArtist artist = response.Content; // Corrected property for artist details
                    await DisplayArtistInfo(artist); // Fixed method placement and call
                }
                else
                {
                    rtbResults.Text = "Artist not found.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error has occurred: {ex.Message}");
            }
        }
                private async Task DisplayArtistInfo(LastArtist artist)
                {
                    rtbResults.Clear();
                    rtbResults.AppendText($"Artist: {artist.Name}\n");
                    rtbResults.AppendText($"Listeners: {artist.Stats.Listeners}\n");
                    rtbResults.AppendText($"Biography: {artist.Bio.Summary}\n\n");
                    TextBoxArtistName.AppendText($"{artist.Name}");
                    rtbTopTracks.AppendText("Top Tracks:\n\n\n");
                    var topTracksResponse = await _client.Artist.GetTopTracksAsync(artist.Name);

                    if (topTracksResponse.Success)
                    {
                        foreach (var track in topTracksResponse.Content.Take(5)) // Corrected track list access
                        {
                            rtbTopTracks.AppendText($"-  {track.Name}\n\n");
                        }
                    }
                    else
                    {
                        rtbTopTracks.AppendText("Could not retrieve top tracks.");
                    }
            var albumResponse = await _client.Artist.GetTopAlbumsAsync(artist.Name);
            if (albumResponse.Success)
            {
                rtbTopTracks.AppendText("\n\n\nTop Albums:\n\n\n");
                foreach (var album in albumResponse.Content.Take(100)) // Corrected track list access
                {
                    rtbTopTracks.AppendText($"-  {album.Name}\n\n");
                }
            }
            else
            {
                rtbTopTracks.AppendText("Could not retrieve top tracks.");
            }

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void rtbResults_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtArtistName.Clear();
            TextBoxArtistName.Clear();
            rtbTopTracks.Clear();
            rtbResults.Clear();
        }

        private void txtArtistName_TextChanged(object sender, EventArgs e)
        {

        }
    }
        }