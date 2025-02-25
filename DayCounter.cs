using UnityEngine;
using TMPro;
using System.Collections;

/*
 * DayCounter 
 * Day counter continuously increases.
 * Update text from Day 1 to Month 5
 * Flashes Day # to draw attention to the counter.
 */
public class DayCounter : MonoBehaviour
{
    public TMP_Text dayText; // Reference to the TextMeshPro component
    private int currentDay = 1;    // Start on Day 1
    private int currentMonth = 0; // Start with no month displayed
    public float dayDuration = 1f; // Duration of each day in seconds

    private float timer;
    private bool isInMonthMode = false; // Tracks whether we are in month mode

    void Start()
    {
        UpdateDayText();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= dayDuration)
        {
            timer = 0;

            if (!isInMonthMode)
            {
                AdvanceDay();
            }
            else
            {
                AdvanceMonth();
            }
            if (ShouldBlink()) // Only blink at the beginning before fast day counts
            {
                StartCoroutine(BlinkEffect());
            }
            else
            {
                UpdateDayText(); // Directly update text without blinking
            }
        }
    }

    // Advance to the next day or switch to months
    private void AdvanceDay()
    {
        if (currentDay < 30)
        {
            currentDay++;

            // Speed up days for days 7–30
            if (currentDay >= 7 && currentDay <= 30)
            {
                dayDuration = 0.2f; // Shorten the duration for faster days
            }
        }
        else
        {
            // Switch to month mode after Day 30
            isInMonthMode = true;
            currentDay = 0; // Reset the day count
            currentMonth = 1; // Start at Month 1
            dayDuration = 1f; // Reset duration to 1 second
        }
    }

    // Advance to the next month
    private void AdvanceMonth()
    {
        if (currentMonth < 5)
        {
            currentMonth++;
        }
    }

    // Update the text on the UI
    private void UpdateDayText()
    {
        if (!isInMonthMode)
        {
            dayText.text = "Day " + currentDay; // Show day count
        }
        else
        {
            dayText.text = "Month " + currentMonth; // Show month count
        }
    }

    /*
     * ShouldBlink()
     * Blinking draws the user's attention to the object,
     * however, I want to avoid a strobe effect, so I disabled it when the tracker speeds up.
     *
     */
    private bool ShouldBlink()
    {
        // Disable blink when either sped-up days (7–30) or day-to-month transition
        if ((currentDay >= 7 && currentDay <= 30 && !isInMonthMode)
            || (currentDay == 0 && isInMonthMode))
        {
            return false;
        }

        return true;
    }

    // Coroutine for the blink effect
    private IEnumerator BlinkEffect()
    {
        string staticText = isInMonthMode ? "Month " : "Day ";
        string numberText = isInMonthMode ? currentMonth.ToString() : currentDay.ToString();

        // Hide the number by making it transparent
        dayText.text = staticText + "<color=#00000000>" + numberText + "</color>";

        // Wait for a brief moment
        yield return new WaitForSeconds(0.1f);

        // Show the updated number
        dayText.text = staticText + numberText;
    }
}
