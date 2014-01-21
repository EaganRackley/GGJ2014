using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

/**
 * @class   TextBoxDialog
 * @brief   Displays a text box using the designer properties specified
 * @author  Eagan
 * @date    1/24/2013
 */
public class TextBoxDialog : TransitionObject
{
    // Designer Properties:
    public Font myFont = new Font();
    public int myFontSize = 12;
    public Color myFontColor = new Color();
    public Texture myBackground;
    public Texture myAvatar;
    public AudioSource myTextBlip = new AudioSource();
    public float myPromptFlashingSpeed = 1.0f;
    public float myTextSpeed = 0.1f;
    public List<String> myLines = new List<String>();

    // Private attributes:
    private const float kNumberOfGridsHigh = 10.0f;
    private int myLastStringIndex = 0;
    private int myCurrentLine = 0;
    private int myLinesToShow = 0;
    private StringBuilder myDisplayString = new StringBuilder();
    private float myPromptTimer = 0;
    private float myTextTimer = 0;
    private bool myPromptStateOn = false;

    // Operations:
   
    /**
     * @fn  void ShowTextBox(int startingLine, int numberOfLines)
     * @brief   Display the starting line of text and continues to display text on keypress until the
     *          total number of lines have been displayed (or there are no lines remaining to
     *          display).
     * @author  Eagan
     * @date    1/24/2013
     * @param   startingLine    The starting line.
     * @param   numberOfLines   Number of lines to show before hiding the dialog.
     */
    public void ShowTextBox(int startingLine, int numberOfLines)
    {
        if (myLinesToShow <= 0)
        {
            myCurrentLine = startingLine;
            myLinesToShow = numberOfLines;
            myLastStringIndex = 0;
            myDisplayString.Remove(0, myDisplayString.Length);
        }
    }

    /**
     * @fn  public bool TextBoxIsVisible()
     * @brief   Indicates whether the current text box is being displayed or not
     * @author  Eagan
     * @date    1/24/2013
     * @return  true if a dialog is already visible, otherwise false.
     */
    public bool TextBoxIsVisible()
    {
        if(myLinesToShow > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /**
     * @fn  void HandleNextLine()
     * @brief   Shows the next line of text [if exists].
     * @author  Eagan
     * @date    1/24/2013
     */
    void HandleNextLine()
    {
        myLinesToShow--;
        if (myCurrentLine < (myLines.Count - 1) )
        {
            myCurrentLine++;
        }
        myLastStringIndex = 0;
        myDisplayString.Remove(0, myDisplayString.Length);    
    }

    /**
     * @fn  bool HandleGradualTextDisplay()
     * @brief   For each frame appends a word to the display text until the entire string is
     *          displayed.
     * @author  Eagan
     * @date    1/24/2013
     * @return  true if [all text is displayed], otherwise false.
     */
    bool HandleGradualTextDisplay()
    {
        bool returnValue = false;

        DebugUtils.Assert(myCurrentLine < myLines.Count, "Current line is " + myCurrentLine.ToString() + " which is great than the total lines of: " + myLines.Count.ToString() + "!");
        int targetLength = (myLines[myCurrentLine].Length - 1);
        // populate myDisplayString using words between spaces, adding one word per frame.        
        if ( (myLastStringIndex < targetLength) && (myLastStringIndex > -1) && (myCurrentLine < myLines.Count) )
        {
            
            // Handle our text timer, and don't append new words until it specifies that we can.
            myTextTimer += Time.deltaTime;
            if (myTextTimer > myTextSpeed)
            {
                myTextTimer = 0.0f;
                int previousIndex = (myLastStringIndex == 0) ? 0 : (myLastStringIndex + 1);
                myLastStringIndex = myLines[myCurrentLine].IndexOf(" ", previousIndex);
                int copyLength = (myLastStringIndex - previousIndex);
                if ((myLastStringIndex >= targetLength) || (myLastStringIndex <= -1))
                {
                    copyLength = (targetLength - previousIndex);
                }
                myDisplayString.Append(myLines[myCurrentLine].Substring(previousIndex, copyLength));
                myDisplayString.Append(" ");
                
                // Play a sound indicating the addition of new text
                if(myTextBlip.isPlaying == false)
                {
                    myTextBlip.Play();
                }
                
            }
        }
        else
        {
            returnValue = true;
        }


        return returnValue;
    }

    /**
     * @fn  void HandleFlashingPrompt(float xLocation, float yLocation, bool readyForPrompt,
     *      float unitSize, GUIStyle style)
     * @brief   Handle flashing prompt.
     * @author  Eagan
     * @date    1/24/2013
     * @param   xLocation       The x coordinate location.
     * @param   yLocation       The y coordinate location.
     * @param   readyForPrompt  true if prompt is ready to display, otherwise prompt will be hidden.
     * @param   unitSize        Size of the unit (e.g. one square on a piece of graph paper.
     * @param   style           The style.
     */
    void HandleFlashingPrompt(float xLocation, float yLocation, bool readyForPrompt, float unitSize, GUIStyle style)
    {
        // After a second has passed swap prompt from on to off
        myPromptTimer += Time.deltaTime;
        if (myPromptTimer > myPromptFlashingSpeed)
        {
            myPromptStateOn = !myPromptStateOn;
            myPromptTimer = 0.0f;
        }

        Rect textRect = new Rect((xLocation + 14.5f * unitSize), (yLocation + 7.5f * unitSize), unitSize * 4.0f, unitSize * 2.0f);

        // If the dialog is ready to show a prompt and the flashing state is on
        if ((readyForPrompt == true) && (myPromptStateOn == true))
        {
            GUI.Label(textRect, "[Enter]", style);
        }
    }

    /**
     * @fn  void RenderTextDialog()
     * @brief   Renders the text dialog.
     * @author  Eagan
     * @date    1/24/2013
     */
    void RenderTextDialog()
    {
        float unitSize = (myBackground.height / kNumberOfGridsHigh);
        float xLocation = ((Screen.width / 2.0f) - (myBackground.width / 2.0f));
        float yLocation = ((Screen.height / 2.0f) - (myBackground.height / 2.0f));

        // Draw the background image for the dialog.
        Rect boxRect = new Rect(xLocation, yLocation, myBackground.width, myBackground.height);
        GUI.Label(boxRect, myBackground);

        // Calculate the avatar offset and size based on the relative size of the image
        float avatarOffset = (unitSize * 2.5f);
        float avatarSize = (unitSize * 5.0f);
        Rect avatarRect = new Rect(boxRect.x + avatarOffset, boxRect.y + avatarOffset, avatarSize, avatarSize);
        GUI.Label(avatarRect, myAvatar);

        // Populate the label string we're displaying until it displays the full text.
        bool textIsDisplayed = HandleGradualTextDisplay();

        // Now draw the text in our label in the appropriate location.
        Rect textRect = new Rect((avatarRect.x + avatarRect.width + unitSize), avatarRect.y, unitSize * 9.0f, unitSize * 6.0f);
        GUIStyle style = new GUIStyle();
        style.font = myFont;
        style.fontSize = myFontSize;
        style.wordWrap = true;
        style.normal.textColor = myFontColor;
        GUI.Label(textRect, myDisplayString.ToString(), style);

        // Handle a flashing prompt once our text dialog is displayed
        HandleFlashingPrompt(xLocation, yLocation, textIsDisplayed, unitSize, style);

        // Wait for the space bar to be pressed and released, once that happens increment to the next line of
        // text and decrement the total number of lines to show        
        if ( (textIsDisplayed == true) && (Input.GetKeyUp("return") == true) )
        {
            HandleNextLine();
        }
    }

    /// <summary>
    /// Raises the GUI event.
    /// </summary>
    void OnGUI()
    {
        if (myLinesToShow > 0)
        {
            // Render the text dialog with flashing prompt.
            RenderTextDialog();            
        }
    }

}
