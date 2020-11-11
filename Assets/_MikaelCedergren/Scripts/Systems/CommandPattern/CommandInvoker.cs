using System.Collections.Generic;
using UnityEngine;

public class CommandInvoker {

    private Queue<ICommand> commandBuffer;
    private Queue<ICommand> previewCommandBuffer;
    private List<ICommand> commandHistory;
    private int counter;

    public CommandInvoker() {
        commandBuffer = new Queue<ICommand>();
        commandHistory = new List<ICommand>();
        previewCommandBuffer = new Queue<ICommand>();
        counter = 0;
    }

    public void Add(ICommand command) {
        while (commandHistory.Count > counter) {
            commandHistory.RemoveAt(counter);
        }
        commandHistory.Add(command);
        counter++;
    }

    public void Execute() {
        for (int i = 0; i < counter; i++) {
            commandBuffer.Enqueue(commandHistory[i]);
        }
        commandHistory.Clear();
        previewCommandBuffer.Clear();
        counter = 0;

        while (commandBuffer.Count > 0) {
            ICommand command = commandBuffer.Dequeue();
            command.Execute();
        }
    }

    public void Preview() {
        for (int i = 0; i < counter; i++) {
            previewCommandBuffer.Enqueue(commandHistory[i]);
        }

        while (previewCommandBuffer.Count > 0) {
            ICommand command = previewCommandBuffer.Dequeue();
            command.Preview();
        }
    }

    public void RevokePreview() {
        for (int i = 0; i < counter; i++) {
            previewCommandBuffer.Enqueue(commandHistory[i]);
        }

        while (previewCommandBuffer.Count > 0) {
            ICommand command = previewCommandBuffer.Dequeue();
            command.RevokePreview();
        }
    }


    public void Undo() {
        if (counter > 0) {
            counter--;
            commandHistory[counter].Undo();
        }
    }

    public void Redo() {
        if (counter < commandHistory.Count) {
            commandHistory[counter].Preview();
            counter++;
        }
    }

    public void Destroy() {
        for (int i = 0; i < counter; i++) {
            previewCommandBuffer.Enqueue(commandHistory[i]);
        }

        while (previewCommandBuffer.Count > 0) {
            ICommand command = previewCommandBuffer.Dequeue();
            command.RevokePreview();
        }
    }

}
