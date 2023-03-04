using System;
public class HappinessSystem
{
    public event EventHandler OnHappinessChanged;
    private int happiness;
    private int maxHappiness;
    public HappinessSystem(int happinessMax) {
        this.maxHappiness = happinessMax;
        happiness = happinessMax;
    }
    public int getHappiness() {
        return happiness;
    }
    public float getHappinessPercent() {
        return (float) happiness / maxHappiness;
    }
    public void decreaseHappiness(int amount) {
        happiness -= amount;
        if (happiness < 0) happiness = 0;
        if (OnHappinessChanged != null) OnHappinessChanged(this, EventArgs.Empty);
    }
    public void increaseHappiness(int amount) {
        happiness += amount;
        if (happiness > maxHappiness) happiness = maxHappiness;
        if (OnHappinessChanged != null) OnHappinessChanged(this, EventArgs.Empty);
    }
}
