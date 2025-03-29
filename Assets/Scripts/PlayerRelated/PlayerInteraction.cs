using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public List<Interaction> interactions;
    public PlayerCharacter M_PlayerInfo;
    public Image interactButton;
    public Sprite defaultButton;
    public Color32 interactableColor, unInteractableColor;
    public Transform cooldownScaler;
    public bool canInteract = true, canAddInteract = true;
    private void Update()
    {
        if (interactions.Count != 0 && canInteract)
        {
            interactButton.color = interactableColor;
            if (interactions[0].remove == false)
            {
                cooldownScaler.localScale = new Vector3(1-Mathf.Min(interactions[0].counter/interactions[0].cooldown, 1), 1, 1);
            }
            else cooldownScaler.localScale = new Vector3(0, 1, 1);
        }
        else
        {
            interactButton.color = unInteractableColor;
            cooldownScaler.localScale = new Vector3(0, 1, 1);
        }
    }
    public void Interact()
    {
        if (!canInteract || interactions.Count == 0) return;
        Interaction a = interactions[0];
        if (interactions[0].remove)
        {
            interactions.Remove(interactions[0]);
            RefreshIcon();
        }
        else
        {
            if (interactions[0].counter < interactions[0].cooldown) return;
        }
        a.Interact();
    }
    public void Refresh()
    {
        int a = interactions.Count;
        for(int i = interactions.Count - 1; i >= 0; i--)
        {
            if (interactions[i].requireType) interactions.Remove(interactions[i]);
        }
    }
    public void Add(Interaction interaction)
    {
        if (!canAddInteract || interactions.Contains(interaction) || !interaction.interactable) return;
        if (interaction.requireType && interaction.requiredType != M_PlayerInfo.animalType)
        {
            return;
        }
        if (interactions.Count == 0)
        {
            interactions.Add(interaction);
            RefreshIcon();
            return;
        }
        for(int i = interactions.Count - 1; i >= 0; i--)
        {
            if (interactions[i].priority > interaction.priority)
            {
                interactions.Insert(i, interaction);
                return;
            }
        }
        interactions.Insert(0, interaction);
        RefreshIcon();
    }
    public void Remove(Interaction interaction) 
    {
        if (interactions.Contains(interaction)) interactions.Remove(interaction);
        RefreshIcon();
    }
    public void RequestInteract(Interaction interaction)
    {
        if (interaction.requireType && interaction.requiredType != M_PlayerInfo.animalType) return;
        interaction.Interact();
    }
    void RefreshIcon()
    {
        if (interactions.Count == 0 || interactions[0].uniqueButton == null) interactButton.sprite = defaultButton;
        else interactButton.sprite = interactions[0].uniqueButton;
    }
}