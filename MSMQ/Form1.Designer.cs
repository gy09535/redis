namespace Msmq.PerfermanceTest
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSend = new System.Windows.Forms.Button();
            this.btnReceive = new System.Windows.Forms.Button();
            this.btnSendWithTracnsactation = new System.Windows.Forms.Button();
            this.btnReceiveWithTracnsactation = new System.Windows.Forms.Button();
            this.lblBeginTime = new System.Windows.Forms.Label();
            this.lblEndTime = new System.Windows.Forms.Label();
            this.btnAsyncReceive = new System.Windows.Forms.Button();
            this.btnAsyncReceiveWithTracnsactation = new System.Windows.Forms.Button();
            this.btnQueueListener = new System.Windows.Forms.Button();
            this.btnStopQueueListener = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(27, 29);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 0;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnReceive
            // 
            this.btnReceive.Location = new System.Drawing.Point(224, 29);
            this.btnReceive.Name = "btnReceive";
            this.btnReceive.Size = new System.Drawing.Size(75, 23);
            this.btnReceive.TabIndex = 1;
            this.btnReceive.Text = "Receive";
            this.btnReceive.UseVisualStyleBackColor = true;
            this.btnReceive.Click += new System.EventHandler(this.btnReceive_Click);
            // 
            // btnSendWithTracnsactation
            // 
            this.btnSendWithTracnsactation.Location = new System.Drawing.Point(27, 82);
            this.btnSendWithTracnsactation.Name = "btnSendWithTracnsactation";
            this.btnSendWithTracnsactation.Size = new System.Drawing.Size(165, 23);
            this.btnSendWithTracnsactation.TabIndex = 2;
            this.btnSendWithTracnsactation.Text = "SendWithTracnsactation";
            this.btnSendWithTracnsactation.UseVisualStyleBackColor = true;
            this.btnSendWithTracnsactation.Click += new System.EventHandler(this.btnSendWithTracnsactation_Click);
            // 
            // btnReceiveWithTracnsactation
            // 
            this.btnReceiveWithTracnsactation.Location = new System.Drawing.Point(224, 82);
            this.btnReceiveWithTracnsactation.Name = "btnReceiveWithTracnsactation";
            this.btnReceiveWithTracnsactation.Size = new System.Drawing.Size(165, 23);
            this.btnReceiveWithTracnsactation.TabIndex = 3;
            this.btnReceiveWithTracnsactation.Text = "ReceiveWithTracnsactation";
            this.btnReceiveWithTracnsactation.UseVisualStyleBackColor = true;
            this.btnReceiveWithTracnsactation.Click += new System.EventHandler(this.btnReceiveWithTracnsactation_Click);
            // 
            // lblBeginTime
            // 
            this.lblBeginTime.AutoSize = true;
            this.lblBeginTime.Location = new System.Drawing.Point(25, 197);
            this.lblBeginTime.Name = "lblBeginTime";
            this.lblBeginTime.Size = new System.Drawing.Size(59, 12);
            this.lblBeginTime.TabIndex = 4;
            this.lblBeginTime.Text = "beginTime";
            // 
            // lblEndTime
            // 
            this.lblEndTime.AutoSize = true;
            this.lblEndTime.Location = new System.Drawing.Point(412, 197);
            this.lblEndTime.Name = "lblEndTime";
            this.lblEndTime.Size = new System.Drawing.Size(47, 12);
            this.lblEndTime.TabIndex = 5;
            this.lblEndTime.Text = "endTime";
            // 
            // btnAsyncReceive
            // 
            this.btnAsyncReceive.Location = new System.Drawing.Point(414, 28);
            this.btnAsyncReceive.Name = "btnAsyncReceive";
            this.btnAsyncReceive.Size = new System.Drawing.Size(90, 23);
            this.btnAsyncReceive.TabIndex = 6;
            this.btnAsyncReceive.Text = "AsyncReceive";
            this.btnAsyncReceive.UseVisualStyleBackColor = true;
            this.btnAsyncReceive.Click += new System.EventHandler(this.btnAsyncReceive_Click);
            // 
            // btnAsyncReceiveWithTracnsactation
            // 
            this.btnAsyncReceiveWithTracnsactation.Location = new System.Drawing.Point(414, 82);
            this.btnAsyncReceiveWithTracnsactation.Name = "btnAsyncReceiveWithTracnsactation";
            this.btnAsyncReceiveWithTracnsactation.Size = new System.Drawing.Size(211, 23);
            this.btnAsyncReceiveWithTracnsactation.TabIndex = 7;
            this.btnAsyncReceiveWithTracnsactation.Text = "AsyncReceiveWithTracnsactation";
            this.btnAsyncReceiveWithTracnsactation.UseVisualStyleBackColor = true;
            this.btnAsyncReceiveWithTracnsactation.Click += new System.EventHandler(this.btnAsyncReceiveWithTracnsactation_Click);
            // 
            // btnQueueListener
            // 
            this.btnQueueListener.Location = new System.Drawing.Point(27, 145);
            this.btnQueueListener.Name = "btnQueueListener";
            this.btnQueueListener.Size = new System.Drawing.Size(211, 23);
            this.btnQueueListener.TabIndex = 8;
            this.btnQueueListener.Text = "Start QueueListener";
            this.btnQueueListener.UseVisualStyleBackColor = true;
            this.btnQueueListener.Click += new System.EventHandler(this.btnQueueListener_Click);
            // 
            // btnStopQueueListener
            // 
            this.btnStopQueueListener.Location = new System.Drawing.Point(414, 145);
            this.btnStopQueueListener.Name = "btnStopQueueListener";
            this.btnStopQueueListener.Size = new System.Drawing.Size(211, 23);
            this.btnStopQueueListener.TabIndex = 9;
            this.btnStopQueueListener.Text = "Stop QueueListener";
            this.btnStopQueueListener.UseVisualStyleBackColor = true;
            this.btnStopQueueListener.Click += new System.EventHandler(this.btnStopQueueListener_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(655, 228);
            this.Controls.Add(this.btnStopQueueListener);
            this.Controls.Add(this.btnQueueListener);
            this.Controls.Add(this.btnAsyncReceiveWithTracnsactation);
            this.Controls.Add(this.btnAsyncReceive);
            this.Controls.Add(this.lblEndTime);
            this.Controls.Add(this.lblBeginTime);
            this.Controls.Add(this.btnReceiveWithTracnsactation);
            this.Controls.Add(this.btnSendWithTracnsactation);
            this.Controls.Add(this.btnReceive);
            this.Controls.Add(this.btnSend);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnReceive;
        private System.Windows.Forms.Button btnSendWithTracnsactation;
        private System.Windows.Forms.Button btnReceiveWithTracnsactation;
        private System.Windows.Forms.Label lblBeginTime;
        private System.Windows.Forms.Label lblEndTime;
        private System.Windows.Forms.Button btnAsyncReceive;
        private System.Windows.Forms.Button btnAsyncReceiveWithTracnsactation;
        private System.Windows.Forms.Button btnQueueListener;
        private System.Windows.Forms.Button btnStopQueueListener;
    }
}

