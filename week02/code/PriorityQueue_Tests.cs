using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Add three items with different priorities, with the highest-priority
    // item placed at the end of the queue.
    // Expected Result: "High" should be removed first, followed by "Medium" and "Low".
    // Defect(s) Found:
    // The original loop did not examine the last item in the queue.
    // The original Dequeue method returned an item without removing it.
    public void TestPriorityQueue_HighestPriority()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("Medium", 5);
        priorityQueue.Enqueue("High", 10);

        Assert.AreEqual("High", priorityQueue.Dequeue());
        Assert.AreEqual("Medium", priorityQueue.Dequeue());
        Assert.AreEqual("Low", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Add multiple items where two items share the highest priority.
    // Expected Result: The first item added with the highest priority should be
    // removed before the later item with the same priority.
    // Defect(s) Found:
    // The original comparison used >=, which selected the last item with the
    // highest priority instead of preserving FIFO order.
    public void TestPriorityQueue_SamePriorityUsesFifo()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("First High", 10);
        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("Second High", 10);

        Assert.AreEqual("First High", priorityQueue.Dequeue());
        Assert.AreEqual("Second High", priorityQueue.Dequeue());
        Assert.AreEqual("Low", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Try to remove an item from an empty priority queue.
    // Expected Result: An InvalidOperationException should be thrown with the
    // exact message "The queue is empty."
    // Defect(s) Found:
    // No defect was found in the empty queue exception handling.
    public void TestPriorityQueue_EmptyQueue()
    {
        var priorityQueue = new PriorityQueue();

        var exception = Assert.ThrowsException<InvalidOperationException>(
            () => priorityQueue.Dequeue()
        );

        Assert.AreEqual("The queue is empty.", exception.Message);
    }
}