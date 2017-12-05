;; sample function to run a shell command and put results in a buffer
(defun test-command (args)
  (interactive)
  (shell-command (concat "ls " args) "*X SQL Results*")
  )
