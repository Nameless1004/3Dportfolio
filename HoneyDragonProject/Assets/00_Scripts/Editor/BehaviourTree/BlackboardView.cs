using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class BlackboardView : VisualElement
{
    public new class UxmlFactory : UxmlFactory<BlackboardView, UxmlTraits> { }

    public BlackboardView(){}
    private List<Label> labels = new List<Label>();

    public void UpdateBlackboard(BehaviourTreeView treeView)
    {
        if(Application.isPlaying)
        {
            Clear();
            var tree = treeView.tree;
            if(tree != null && tree.Blackboard != null)
            {
                var dataContext = tree.Blackboard.GetDataContext();
                if(dataContext != null)
                {
                    //labels.ForEach(label => contentContainer.Remove(label));
                    //labels.Clear();

                    foreach (var x in dataContext)
                    {
                        var valueName = x.Value == null ? "object" : x.Value.GetType().Name;
                        var label = new Label($"Key: {x.Key} / Value: ({valueName}) {x.Value}");
                   //     labels.Add(label);
                        contentContainer.Add(label);
                    }
                }
            }
            
        }
    }
}
