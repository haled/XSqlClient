Some text

select * from access where accessid = 311

more text

(defun my-command ()
  (interactive)
  (message "You called my command.")
)



(print-region-text)

(call-process "pwd" nil t)/Users/darrenhale/code/github_repos/XSqlClient

; this call does not yet work correctly
; it is supposed to execute the query above
(call-process-region 12 53 "~/code/github_repos/XSqlClient/xsqlclient.sh" nil t)





(call-process-region 12 53 "cat" nil t)

(defun display-region-text ()
  (interactive
  (if (use-region-p)
    (setq text (buffer-substring (region-beginning) (region-end)))
    (setq text ""))
  (defvar msg "")
  (if (not (eq text ""))
      (setq msg text)
  )
  (message "The select text was: |%s|" msg)
)



(defun execute-command-on-region (&optional beg end)
  "get region or \"empty string\" if none highlighted"
  (interactive (if (use-region-p)
                   (list (region-beginning) (region-end))
                 (list nil nil)))
  ;; the stuff above is in the "interactive" command
  ;; the stuff below is separate
  ;(message "%s" (if (and beg end)
  ;                  (buffer-substring-no-properties beg end)
  ;                "empty string"))
  (call-process-region beg end "cat" nil "*Result*")
  (switch-to-buffer "*Result*")
)
