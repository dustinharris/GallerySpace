using System.IO;
using System.Text;
using System.Xml;
using UnityEngine;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

public class WebScraper : MonoBehaviour {
    private string webpathstring;
    private List<Uri> listUri;
    private List<Texture2D> listTextures;
    public string imageurl;
    public FrameManager imageFrameManager;

    public void Start()
    {
        // Load frame manager
        imageFrameManager = new FrameManager();
        imageFrameManager.InitialFrameLoad();

        // Get HTML
        webpathstring = "http://www.dustinharris.info/artgallery/15.html";
        var firsturi = new Uri(webpathstring);

        // Create list of textures
        listTextures = new List<Texture2D>();

        // Create list of uris
        listUri = new List<Uri>();

        // Add first uri to list
        listUri.Add(firsturi);
        //Debug.Log("First entry: " + listUri[0]);
        
        Managers.HTML.GetHTML(OnHTMLPage, webpathstring);
    }

    private void OnHTMLPage(string htmlstring)
    {
        // filePath is a path to a file containing the html
        HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();

        // There are various options, set as needed
        htmlDoc.OptionFixNestedTags = true;

        // Parse HTML
        htmlDoc.LoadHtml(htmlstring);

        // Get all images on the main page
        foreach (HtmlNode anchornode in htmlDoc.DocumentNode.SelectNodes("//img[@src]")) {
            // Get url as string
            imageurl = anchornode.Attributes["src"].Value.ToString();
            
            if(!imageurl.StartsWith("http:"))
            {
                imageurl = "http:" + imageurl;
            }

            // Convert string to Uri
            var imageuri = new Uri(imageurl);

            var imagehost = new Uri("http://" + imageuri.Host);

            // Make it absolute if it’s relative
            var uriNext = new Uri(imagehost, imageurl);

            // Get string from Uri
            string urinextstring = uriNext.ToString();


            //Debug.Log(uriNext.ToString());
            //Debug.Log("http://s3.otherpeoplespixels.com/sites/8874/assets/dabjxgDU5ME3zYTK.jpg");
            Managers.Images.GetWebImage(OnWebImage, urinextstring);
        }
    
        Debug.Log("finished");
    }

    private void OnWebImage(Texture2D image)
    {
        // Add texture to list
        listTextures.Add(image);
        Debug.Log("Successfully added image to list");

        // Logging info
        Debug.Log(listTextures.Last().width.ToString() + ", " + listTextures.Last().height.ToString());

        // Add to frame
        imageFrameManager.AddImageToFrame(listTextures.Last());
    }
}
