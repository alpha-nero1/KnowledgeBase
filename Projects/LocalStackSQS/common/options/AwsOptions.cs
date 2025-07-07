namespace common.options;

public class SqsOptions
{
    public string QueueUrl { get; set; } = "";
    public string DeadQueueUrl { get; set; } = "";
}

public class AwsOptions
{
    public SqsOptions SQS { get; set; } = null!;
}